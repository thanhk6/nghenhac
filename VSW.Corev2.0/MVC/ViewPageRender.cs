using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using VSW.Core.Global;

namespace VSW.Core.MVC
{
	internal class ViewPageRender
	{
		internal static bool RenderPage(ViewPageBase viewPage, Controller controller, Class @class, string methodName)
		{
			string text = DeviceDetect.Device;
			if (text == "Desktop")
			{
				text = string.Empty;
			}
			bool result;
			if (methodName == controller.DefaultAction)
			{
				string text2 = methodName.Substring(6);
				controller.ViewLayout = text2 + text;
				if (@class.Instance.GetType().GetMethod(methodName, new Type[]
				{
					typeof(string)
				}) != null)
				{
					if (viewPage.CurrentVQSMVC.GetString(0).ToLower() == text2.ToLower())
					{
						@class.CallMethod(methodName, new object[]
						{
							viewPage.CurrentVQSMVC.GetString(1)
						});
					}
					else
					{
						@class.CallMethod(methodName, new object[]
						{
							viewPage.CurrentVQSMVC.GetString(0)
						});
					}
					result = true;
				}
				else if (@class.Instance.GetType().GetMethod(methodName, new Type[]
				{
					typeof(int)
				}) != null)
				{
					if (viewPage.CurrentVQSMVC.GetString(0).ToLower() == text2.ToLower())
					{
						@class.CallMethod(methodName, new object[]
						{
							viewPage.CurrentVQSMVC.GetValue(1).ToInt()
						});
					}
					else
					{
						@class.CallMethod(methodName, new object[]
						{
							viewPage.CurrentVQSMVC.GetValue(0).ToInt()
						});
					}
					result = true;
				}
				else if (@class.Instance.GetType().GetMethod(methodName, new Type[0]) != null)
				{
					@class.CallMethod(methodName);
					result = true;
				}
				else
				{
					result = ViewPageRender.RenderMethod(viewPage, controller, @class, methodName);
				}
			}
			else
			{
				controller.ViewLayout = methodName.Substring(6) + text;
				if (viewPage.CurrentVQSMVC.Count == 1)
				{
					if (@class.Instance.GetType().GetMethod(methodName, new Type[0]) != null)
					{
						@class.CallMethod(methodName);
						result = true;
					}
					else
					{
						result = ViewPageRender.RenderMethod(viewPage, controller, @class, methodName);
					}
				}
				else if (@class.Instance.GetType().GetMethod(methodName, new Type[]
				{
					typeof(int)
				}) != null && viewPage.CurrentVQSMVC.GetValue(1).ToInt() > 0)
				{
					@class.CallMethod(methodName, new object[]
					{
						viewPage.CurrentVQSMVC.GetValue(1).ToInt()
					});
					result = true;
				}
				else if (@class.Instance.GetType().GetMethod(methodName, new Type[]
				{
					typeof(string)
				}) != null)
				{
					@class.CallMethod(methodName, new object[]
					{
						viewPage.CurrentVQSMVC.GetString(1)
					});
					result = true;
				}
				else if (@class.Instance.GetType().GetMethod(methodName, new Type[0]) != null)
				{
					@class.CallMethod(methodName);
					result = true;
				}
				else
				{
					result = ViewPageRender.RenderMethod(viewPage, controller, @class, methodName);
				}
			}
			return result;
		}
		internal static bool RenderPage(ViewPageBase viewPage, Controller controller, Class @class, string methodName, string funcParamPost)
		{
			bool result;
			if (string.IsNullOrEmpty(funcParamPost))
			{
				if (@class.Instance.GetType().GetMethod(methodName, new Type[0]) != null)
				{
					@class.CallMethod(methodName);
					result = true;
				}
				else
				{
					result = ViewPageRender.RenderMethod(viewPage, controller, @class, methodName);
				}
			}
			else
			{
				MethodInfo method = @class.Instance.GetType().GetMethod(methodName, new Type[]
				{
					typeof(int)
				});
				int num = VSW.Core.Global.Convert.ToInt(funcParamPost);
				if (method != null && num > 0)
				{
					@class.CallMethod(methodName, new object[]
					{
						num
					});
					result = true;
				}
				else if (@class.Instance.GetType().GetMethod(methodName, new Type[]
				{
					typeof(string)
				}) != null)
				{
					@class.CallMethod(methodName, new object[]
					{
						funcParamPost
					});
					result = true;
				}
				else if (@class.Instance.GetType().GetMethod(methodName, new Type[]
				{
					typeof(int[])
				}) != null)
				{
					@class.CallMethod(methodName, new object[]
					{
						VSW.Core.Global.Array.ToInts(funcParamPost)
					});
					result = true;
				}
				else if (@class.Instance.GetType().GetMethod(methodName, new Type[]
				{
					typeof(VSW.Core.Global.Object[])
				}) == null)
				{
					result = ViewPageRender.RenderMethod(viewPage, controller, @class, methodName);
				}
				else
				{
					string[] array = funcParamPost.Split(new char[]
					{
						','
					});
					VSW.Core.Global.Object[] array2 = new VSW.Core.Global.Object[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = new VSW.Core.Global.Object(array[i]);
					}
					@class.CallMethod(methodName, new object[]
					{
						array2
					});
					result = true;
				}
			}
			return result;
		}

		internal static bool RenderMethod(ViewPageBase viewPage, Controller controller, Class @class, string methodName)
		{
			MethodInfo method = @class.Instance.GetType().GetMethod(methodName);
			bool result;
			if (method == null)
			{
				result = false;
			}
			else
			{
				ViewPageRender.Class24 class2 = new ViewPageRender.Class24();
				object[] array = null;
				class2.ArrParams = method.GetParameters();
				if (class2.ArrParams != null)
				{
					array = new object[class2.ArrParams.Length];
					ViewPageRender.Class27 class3 = new ViewPageRender.Class27
					{
						Index = 0
					};
					while (class3.Index < class2.ArrParams.Length)
					{
						Type parameterType = class2.ArrParams[class3.Index].ParameterType;
						if (parameterType == typeof(int[]))
						{
							array[class3.Index] = viewPage.PageViewState.GetValue(class2.ArrParams[class3.Index].Name).ToInts();
						}
						else if (parameterType == typeof(long[]))
						{
							array[class3.Index] = viewPage.PageViewState.GetValue(class2.ArrParams[class3.Index].Name).ToLongs();
						}
						else if (parameterType == typeof(double[]))
						{
							array[class3.Index] = viewPage.PageViewState.GetValue(class2.ArrParams[class3.Index].Name).ToDoubles();
						}
						else if (parameterType == typeof(decimal[]))
						{
							array[class3.Index] = viewPage.PageViewState.GetValue(class2.ArrParams[class3.Index].Name).ToDecimals();
						}
						else if (parameterType == typeof(DateTime[]))
						{
							array[class3.Index] = viewPage.PageViewState.GetValue(class2.ArrParams[class3.Index].Name).ToDateTimes();
						}
						else if (parameterType == typeof(bool[]))
						{
							array[class3.Index] = viewPage.PageViewState.GetValue(class2.ArrParams[class3.Index].Name).ToBools();
						}
						else if (parameterType == typeof(string[]))
						{
							array[class3.Index] = viewPage.PageViewState.GetValue(class2.ArrParams[class3.Index].Name).ToStrings();
						}
						else if (parameterType == typeof(HttpPostedFile))
						{
							array[class3.Index] = viewPage.Request.Files[class2.ArrParams[class3.Index].Name];
						}
						else if (!parameterType.IsValueType && parameterType != typeof(string) && parameterType != typeof(DateTime))
						{
							if (parameterType.IsGenericType)
							{
								Type type = parameterType.GetGenericArguments()[0];
								if (!type.IsValueType && type != typeof(string) && type != typeof(DateTime))
								{
									Type type2 = typeof(List<>).MakeGenericType(parameterType.GetGenericArguments());
									array[class3.Index] = Activator.CreateInstance(type2);
									Class class4 = new Class(array[class3.Index]);
									List<string> list = new List<string>();
									list.AddRange(viewPage.PageViewState.AllKeys);
									Predicate<string> predicate = null;
									ViewPageRender.Class26 class5 = new ViewPageRender.Class26
									{
										class27 = class3,
										class24 = class2,
										Index = 0
									};
									while (class5.Index < list.Count)
									{
										if (predicate == null)
										{
											predicate = new Predicate<string>(class5.Exists);
										}
										if (list.Find(predicate) != null)
										{
											object obj = Activator.CreateInstance(type);
											controller.TryUpdateModel<object>(obj, string.Concat(new object[]
											{
												class2.ArrParams[class3.Index].Name,
												"[",
												class5.Index,
												"]."
											}));
											class4.CallMethod("Add", new object[]
											{
												obj
											});
										}
										class5.Index++;
									}
								}
							}
							else
							{
								array[class3.Index] = Activator.CreateInstance(parameterType);
								controller.TryUpdateModel<object>(array[class3.Index]);
							}
						}
						else
						{
							array[class3.Index] = VSW.Core.Global.Convert.AutoValue(viewPage.PageViewState[class2.ArrParams[class3.Index].Name], parameterType);
						}
						class3.Index++;
					}
				}
				@class.CallMethod(methodName, array);
				result = true;
			}
			return result;
		}
		private sealed class Class24
		{
			public ParameterInfo[] ArrParams;
		}		
		private sealed class Class26
		{
			public bool Exists(string key)
			{
				return key.StartsWith(string.Concat(new object[]
				{
					this.class24.ArrParams[this.class27.Index].Name,
					"[",
					this.Index,
					"]."
				}));
			}

			
			public ViewPageRender.Class24 class24;

			
			public ViewPageRender.Class27 class27;

			
			public int Index;
		}

		
		private sealed class Class27
		{
			
			public int Index;
		}
	}
}
