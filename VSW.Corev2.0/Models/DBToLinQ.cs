using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VSW.Core.Models
{
	internal class DBToLinQ<T>
	{
		internal int IndexParams
		{
			get
			{
				return this._indexParams + this._listParams.Count / 2;
			}
			set
			{
				this._indexParams = value;
			}
		}

		public object[] Params
		{
			get
			{
				object[] result;
				if (this._listParams.Count > 0)
				{
					result = this._listParams.ToArray();
				}
				else
				{
					result = null;
				}
				return result;
			}
			set
			{
				if (value != null)
				{
					this._listParams.AddRange(value);
				}
			}
		}


		internal DBToLinQ()
		{
			this._listParams = new List<object>();
		}
		internal DBToLinQ(object[] Params)
		{
			this._listParams = new List<object>();
			this.Params = Params;
		}
		public string Select(Expression<Func<T, object>> exp)
		{
			string result;
			if (exp == null)
			{
				result = "*";
			}
			else
			{
				result = this.CreateQuery(exp);
			}
			return result;
		}
		public string CreateQuery(Expression<Func<T, bool>> exp)
		{
			string result;
			if (exp == null)
			{
				result = string.Empty;
			}
			else
			{
				result = this.CreateQuery(exp.Body);
			}
			return result;
		}
		public string CreateQuery(Expression<Func<T, string, object>> exp)
		{
			string result;
			if (exp == null)
			{
				result = string.Empty;
			}
			else
			{
				string name = exp.Parameters[1].Name;
				result = this.CreateQuery(exp.Body).Replace(",", " " + name + ",") + " " + name;
			}
			return result;
		}
		public string OrderAsc(Expression<Func<T, object>> exp)
		{
			return this.CreateQuery(exp).Replace(",", " ASC,") + " ASC";
		}

		
		public string OrderDesc(Expression<Func<T, object>> exp)
		{
			return this.CreateQuery(exp).Replace(",", " DESC,") + " DESC";
		}
		private Expression Lambda(Expression exp)
		{
			Expression result;
			if (exp.NodeType == ExpressionType.Constant)
			{
				result = exp;
			}
			else
			{
				Type type = exp.Type;
				if (type.IsValueType)
				{
					exp = Expression.Convert(exp, typeof(object));
				}
				result = Expression.Constant(Expression.Lambda<Func<object>>(exp, new ParameterExpression[0]).Compile()(), type);
			}
			return result;
		}

		private string CreateQuery(Expression exp)
		{
			string result;
			if (exp.NodeType == ExpressionType.Lambda)
			{
				LambdaExpression lambdaExpression = (LambdaExpression)exp;
				result = this.CreateQuery(lambdaExpression.Body);
			}
			else if (exp.NodeType == ExpressionType.And)
			{
				BinaryExpression binaryExpression = (BinaryExpression)exp;
				result = this.CreateQuery(binaryExpression.Left) + "&" + this.CreateQuery(binaryExpression.Right);
			}
			else if (exp.NodeType == ExpressionType.Or)
			{
				BinaryExpression binaryExpression2 = (BinaryExpression)exp;
				result = this.CreateQuery(binaryExpression2.Left) + "^" + this.CreateQuery(binaryExpression2.Right);
			}
			else if (exp.NodeType == ExpressionType.Equal)
			{
				BinaryExpression binaryExpression3 = (BinaryExpression)exp;
				string text = this.CreateQuery(binaryExpression3.Left);
				string text2 = this.CreateQuery(binaryExpression3.Right);
				if (text == "<NULL>")
				{
					result = text2 + " IS NULL";
				}
				else if (text2 == "<NULL>")
				{
					result = text + " IS NULL";
				}
				else
				{
					result = text + "=" + text2;
				}
			}
			else if (exp.NodeType == ExpressionType.NotEqual)
			{
				BinaryExpression binaryExpression4 = (BinaryExpression)exp;
				string text3 = this.CreateQuery(binaryExpression4.Left);
				string text4 = this.CreateQuery(binaryExpression4.Right);
				if (text3 == "<NULL>")
				{
					result = text4 + " IS NOT NULL";
				}
				else if (text4 == "<NULL>")
				{
					result = text3 + " IS NOT NULL";
				}
				else
				{
					result = text3 + "<>" + text4;
				}
			}
			else if (exp.NodeType == ExpressionType.GreaterThan)
			{
				BinaryExpression binaryExpression5 = (BinaryExpression)exp;
				result = this.CreateQuery(binaryExpression5.Left) + ">" + this.CreateQuery(binaryExpression5.Right);
			}
			else if (exp.NodeType == ExpressionType.GreaterThanOrEqual)
			{
				BinaryExpression binaryExpression6 = (BinaryExpression)exp;
				result = this.CreateQuery(binaryExpression6.Left) + ">=" + this.CreateQuery(binaryExpression6.Right);
			}
			else if (exp.NodeType == ExpressionType.LessThan)
			{
				BinaryExpression binaryExpression7 = (BinaryExpression)exp;
				result = this.CreateQuery(binaryExpression7.Left) + "<" + this.CreateQuery(binaryExpression7.Right);
			}
			else if (exp.NodeType == ExpressionType.LessThanOrEqual)
			{
				BinaryExpression binaryExpression8 = (BinaryExpression)exp;
				result = this.CreateQuery(binaryExpression8.Left) + "<=" + this.CreateQuery(binaryExpression8.Right);
			}
			else if (exp.NodeType == ExpressionType.AndAlso)
			{
				BinaryExpression binaryExpression9 = (BinaryExpression)exp;
				result = string.Concat(new string[]
				{
					"(",
					this.CreateQuery(binaryExpression9.Left),
					") AND (",
					this.CreateQuery(binaryExpression9.Right),
					")"
				});
			}
			else if (exp.NodeType == ExpressionType.OrElse)
			{
				BinaryExpression binaryExpression10 = (BinaryExpression)exp;
				result = string.Concat(new string[]
				{
					"(",
					this.CreateQuery(binaryExpression10.Left),
					") OR (",
					this.CreateQuery(binaryExpression10.Right),
					")"
				});
			}
			else if (exp.NodeType == ExpressionType.MemberAccess)
			{
				MemberExpression memberExpression = (MemberExpression)exp;
				if (memberExpression.Expression != null && memberExpression.Expression.Type == typeof(T) && memberExpression.Expression.NodeType == ExpressionType.Parameter)
				{
					result = "[" + memberExpression.Member.Name + "]";
				}
				else
				{
					ConstantExpression exp2 = (ConstantExpression)this.Lambda(memberExpression);
					result = this.CreateQuery(exp2);
				}
			}
			else
			{
				if (exp.NodeType == ExpressionType.Constant)
				{
					ConstantExpression constantExpression = (ConstantExpression)exp;
					if (constantExpression.Value == null)
					{
						return "<NULL>";
					}
					if (constantExpression.Value is string || constantExpression.Value is DateTime)
					{
						int indexParams = this.IndexParams;
						this._listParams.Add("@p100" + indexParams.ToString());
						this._listParams.Add(constantExpression.Value);
						return "@p100" + indexParams.ToString();
					}
					if (constantExpression.Value is bool)
					{
						if (!(bool)constantExpression.Value)
						{
							return "0";
						}
						return "1";
					}
					else if (constantExpression.Type.IsValueType)
					{
						return constantExpression.Value.ToString();
					}
				}
				else
				{
					if (exp.NodeType == ExpressionType.Convert)
					{
						UnaryExpression unaryExpression = (UnaryExpression)exp;
						return this.CreateQuery(unaryExpression.Operand);
					}
					if (exp.NodeType == ExpressionType.Call)
					{
						MethodCallExpression methodCallExpression = (MethodCallExpression)exp;
						if (methodCallExpression.Method.Name == "Contains" || methodCallExpression.Method.Name == "StartsWith" || methodCallExpression.Method.Name == "EndsWith")
						{
							string text5 = this.CreateQuery(methodCallExpression.Object);
							string item = ((ConstantExpression)this.Lambda(methodCallExpression.Arguments[0])).Value.ToString();
							int indexParams2 = this.IndexParams;
							this._listParams.Add("@p100" + indexParams2.ToString());
							this._listParams.Add(item);
							if (methodCallExpression.Method.Name == "Contains")
							{
								return string.Concat(new object[]
								{
									text5,
									" COLLATE SQL_Latin1_General_CP1_CI_AS LIKE '%' + @p100",
									indexParams2,
									" + '%'"
								});
							}
							if (methodCallExpression.Method.Name == "StartsWith")
							{
								return string.Concat(new object[]
								{
									text5,
									" COLLATE SQL_Latin1_General_CP1_CI_AS LIKE '' + @p100",
									indexParams2,
									" + '%'"
								});
							}
							return string.Concat(new object[]
							{
								text5,
								" COLLATE SQL_Latin1_General_CP1_CI_AS LIKE '%' + @p100",
								indexParams2,
								" + ''"
							});
						}
						else if (methodCallExpression.Object == null)
						{
							return this.CreateQuery(this.Lambda(methodCallExpression));
						}
					}
					else if (exp.NodeType == ExpressionType.New)
					{
						NewExpression newExpression = (NewExpression)exp;
						string text6 = string.Empty;
						for (int i = 0; i < newExpression.Arguments.Count; i++)
						{
							text6 = text6 + ((text6 == string.Empty) ? string.Empty : ",") + this.CreateQuery(newExpression.Arguments[i]);
						}
						return text6;
					}
				}
				result = string.Empty;
			}
			return result;
		}
		private int _indexParams;
		private List<object> _listParams;
	}
}
