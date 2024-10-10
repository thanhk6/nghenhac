using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using VSW.Core.Global;
using VSW.Core.Interface;
using VSW.Core.Models;
using VSW.Core.MVC;
using VSW.Core.Web;

namespace VSW.Core.Design
{
	public class ViewControlDesign : UserControl
	{
		public string Code { get; set; }
		public string CphName { get; set; }
		public IModuleInterface CurrentModule { get; private set; }
		public Class CurrentObject { get; private set; }
		public int LangID
		{
			get
			{
				return this.ViewPageDesign.CurrentTemplate.LangID;
			}
		}
		public int PosID { get; set; }
		public string ViewControlID
		{
			get
			{
				string result;
				if (this.VSWID != string.Empty)
				{
					result = this.VSWID;
				}
				else
				{
					result = this.Code;
				}
				return result;
			}
		}
		public string ViewControlName
		{
			get
			{
				string result;
				if (this.VSWID == string.Empty)
				{
					result = this.Code;
				}
				else
				{
					result = this.Code + "|" + this.VSWID;
				}
				return result;
			}
		}
		public ViewPageDesign ViewPageDesign
		{
			get
			{
				return this.Page as ViewPageDesign;
			}
		}
		public string VSWID { get; set; }
		public string GetCurrentValue(string fieldName)
		{
			string text = this.GetValueCustom(fieldName);
			if (text == string.Empty)
			{
				text = ((this.CurrentObject.GetField(fieldName) == null) ? string.Empty : this.CurrentObject.GetField(fieldName).ToString());
			}
			return text;
		}
		public PropertyInfo GetPropertyInfo(string fieldName)
		{
			object[] customAttributes = this.CurrentObject.GetFieldInfo(fieldName).GetCustomAttributes(typeof(PropertyInfo), true);
			PropertyInfo result;
			if (customAttributes != null && customAttributes.GetLength(0) > 0)
			{
				result = (PropertyInfo)customAttributes[0];
			}
			else
			{
				result = null;
			}
			return result;
		}
		public string GetValueCustom(string name)
		{
			return this._item.GetValue(this.ViewControlID + "." + name).ToString();
		}
		private void method_0()
		{
			string text = this._item.GetValue("Template_" + this.CphName).ToString();
			char[] separator = new char[]
			{
				','
			};
			string[] array = text.Split(separator);
			int num = System.Array.IndexOf<string>(array, this.ViewControlName);
			if (num > 0)
			{
				string text2 = array[num - 1];
				array[num - 1] = this.ViewControlName;
				array[num] = text2;
			}
			this.designCode = this.designCode.Replace("Template_" + this.CphName + "=" + text, "Template_" + this.CphName + "=" + VSW.Core.Global.Array.ToString(array));
			this.method_5();
			base.Response.Redirect(base.Request.RawUrl);
		}
		private void method_1()
		{
			string text = this._item.GetValue("Template_" + this.CphName).ToString();
			char[] separator = new char[]
			{
				','
			};
			string[] array = text.Split(separator);
			int num = System.Array.IndexOf<string>(array, this.ViewControlName);
			if (num < array.Length - 1)
			{
				string text2 = array[num + 1];
				array[num + 1] = this.ViewControlName;
				array[num] = text2;
			}
			this.designCode = this.designCode.Replace("Template_" + this.CphName + "=" + text, "Template_" + this.CphName + "=" + VSW.Core.Global.Array.ToString(array));
			this.method_5();
			base.Response.Redirect(base.Request.RawUrl);
		}
		private void method_2(string string_4, string string_5, string string_6)
		{
			if (string_5 == "VSWMODULE")
			{
				string_6 = string.Empty;
			}
			string_6 = Regex.Replace(string_6, "[^a-zA-Z0-9]", string.Empty);
			if (!(string_5 != "VSWMODULE") || !(string_6 == string.Empty))
			{
				if (string_5 != "VSWMODULE")
				{
					string[] allKeys = this._item.AllKeys;
					for (int i = 0; i < allKeys.Length; i++)
					{
						if (allKeys[i].StartsWith("Template_"))
						{
							string text = this._item.GetValue(allKeys[i]).ToString();
							char[] separator = new char[]
							{
								','
							};
							string[] array = text.Split(separator);
							for (int j = 0; j < array.Length; j++)
							{
								string b;
								if (array[j].Contains("|"))
								{
									string text2 = array[j];
									char[] separator2 = new char[]
									{
										'|'
									};
									b = text2.Split(separator2)[1];
								}
								else
								{
									b = array[j];
								}
								if (string_6 == b)
								{
									return;
								}
							}
						}
					}
				}
				string text3 = (string_6 == string.Empty) ? string_5 : (string_5 + "|" + string_6);
				if (!this._item.Exists("Template_" + string_4))
				{
					string[] values = new string[]
					{
						"Template_",
						string_4,
						"=",
						text3,
						"\r\n",
						this.designCode
					};
					this.designCode = string.Concat(values);
				}
				else
				{
					string text4 = this._item.GetValue("Template_" + string_4).ToString();
					char[] separator3 = new char[]
					{
						','
					};
					string[] array2 = text4.Split(separator3);
					if (System.Array.IndexOf<string>(array2, text3) > -1)
					{
						return;
					}
					System.Array.Resize<string>(ref array2, array2.Length + 1);
					array2[array2.Length - 1] = text3;
					this.designCode = this.designCode.Replace("Template_" + string_4 + "=" + text4, "Template_" + string_4 + "=" + VSW.Core.Global.Array.ToString(array2));
				}
				this.method_5();
				base.Response.Redirect(base.Request.RawUrl);
			}
		}
		private void method_3()
		{
			string[] allKeys = HttpForm.AllKeys;
			for (int i = 0; i < allKeys.Length; i++)
			{
				if (allKeys[i].StartsWith(this.ViewControlID + "_"))
				{
					string text = allKeys[i].Replace(this.ViewControlID + "_", string.Empty);
					string text2 = allKeys[i].Replace(this.ViewControlID + "_", this.ViewControlID + ".");
					string text3 = HttpForm.GetValue(allKeys[i]).ToString().Trim();
					string str = this._item.GetValue(text2).ToString().Trim();
					bool flag = false;
					bool flag2 = false;
					if (this.CurrentObject.ExistsField(text))
					{
						flag = true;
					}
					if (this.CurrentObject.ExistsProperty(text))
					{
						flag2 = true;
					}
					if (flag || flag2)
					{
						string b = string.Empty;
						if (flag)
						{
							b = ((this.CurrentObject.GetField(text) == null) ? string.Empty : this.CurrentObject.GetField(text).ToString());
						}
						else if (flag2 && text != "ViewLayout")
						{
							b = ((this.CurrentObject.GetProperty(text) == null) ? string.Empty : this.CurrentObject.GetProperty(text).ToString());
						}
						if (text3 != string.Empty && text3 != b)
						{
							if (!this._item.Exists(text2))
							{
								string text4 = this.designCode;
								string[] array = new string[5];
								array[0] = text4;
								string[] array2 = array;
								string[] array3 = array2;
								string text5 = (this.designCode == string.Empty) ? string.Empty : "\r\n";
								array3[1] = text5;
								array2[2] = text2;
								array2[3] = "=";
								array2[4] = text3;
								this.designCode = string.Concat(array2);
							}
							else
							{
								this.designCode = this.designCode.Replace(text2 + "=" + str, text2 + "=" + text3);
							}
						}
						else if (this._item.Exists(text2))
						{
							this.designCode = this.designCode.Replace(text2 + "=" + str + "\n", string.Empty);
							this.designCode = this.designCode.Replace(text2 + "=" + str, string.Empty);
						}
					}
				}
			}
			this.method_5();
			base.Response.Redirect(base.Request.RawUrl);
		}
		private void method_4()
		{
			string text = this._item.GetValue("Template_" + this.CphName).ToString();
			char[] separator = new char[]
			{
				','
			};
			string[] array = text.Split(separator);
			int num = System.Array.IndexOf<string>(array, this.ViewControlName);
			int num2 = -1;
			string[] array2 = new string[array.Length - 1];
			for (int i = 0; i < array.Length; i++)
			{
				if (i != num)
				{
					num2++;
					array2[num2] = array[i];
				}
			}
			this.designCode = this.designCode.Replace("Template_" + this.CphName + "=" + text, "Template_" + this.CphName + "=" + VSW.Core.Global.Array.ToString(array2));
			string string_ = this.ViewControlName;
			if (this.ViewControlName.IndexOf('|') > -1)
			{
				string viewControlName = this.ViewControlName;
				char[] separator2 = new char[]
				{
					'|'
				};
				string_ = viewControlName.Split(separator2)[1];
			}
			this.method_6(string_);
			base.Response.Redirect(base.Request.RawUrl);
		}
		private void method_5()
		{
			this.method_6(string.Empty);
		}
		private void method_6(string string_4)
		{
			if (this.ViewPageDesign.CurrentPage == null)
			{
				if (string_4 != string.Empty)
				{
					string[] allKeys = this._item.AllKeys;
					for (int i = 0; i < allKeys.Length; i++)
					{
						if (allKeys[i].StartsWith(string_4 + "."))
						{
							string text = this.designCode;
							object[] args = new object[]
							{
								allKeys[i],
								"=",
								this._item.GetValue(allKeys[i]),
								"\r\n"
							};
							this.designCode = text.Replace(string.Concat(args), string.Empty);
							string text2 = this.designCode;
							string str = allKeys[i];
							string str2 = "=";
							VSW.Core.Global.Object value = this._item.GetValue(allKeys[i]);
							this.designCode = text2.Replace(str + str2 + ((value != null) ? value.ToString() : null), string.Empty);
						}
					}
				}
				this.ViewPageDesign.CurrentTemplate.Custom = this.designCode;
				this.ViewPageDesign.TemplateService.VSW_Core_CPSave(this.ViewPageDesign.CurrentTemplate);
				return;
			}
			this.designCode = this.designCode.Replace(this.ViewPageDesign.CurrentTemplate.Custom, string.Empty);
			string[] allKeys2 = this.ViewPageDesign.CurrentTemplate.Items.AllKeys;
			for (int j = 0; j < allKeys2.Length; j++)
			{
				string text3 = this.designCode;
				object[] args2 = new object[]
				{
					allKeys2[j],
					"=",
					this.ViewPageDesign.CurrentTemplate.Items.GetValue(allKeys2[j]),
					"\r\n"
				};
				this.designCode = text3.Replace(string.Concat(args2), string.Empty);
				if (!allKeys2[j].StartsWith("Template_"))
				{
					string text4 = this.designCode;
					string str3 = allKeys2[j];
					string str4 = "=";
					VSW.Core.Global.Object value2 = this.ViewPageDesign.CurrentTemplate.Items.GetValue(allKeys2[j]);
					this.designCode = text4.Replace(str3 + str4 + ((value2 != null) ? value2.ToString() : null), string.Empty);
				}
			}
			if (string_4 != string.Empty)
			{
				allKeys2 = this._item.AllKeys;
				for (int k = 0; k < allKeys2.Length; k++)
				{
					if (allKeys2[k].StartsWith(string_4 + "."))
					{
						string text5 = this.designCode;
						object[] args3 = new object[]
						{
							allKeys2[k],
							"=",
							this._item.GetValue(allKeys2[k]),
							"\r\n"
						};
						this.designCode = text5.Replace(string.Concat(args3), string.Empty);
						string text6 = this.designCode;
						string str5 = allKeys2[k];
						string str6 = "=";
						VSW.Core.Global.Object value3 = this._item.GetValue(allKeys2[k]);
						this.designCode = text6.Replace(str5 + str6 + ((value3 != null) ? value3.ToString() : null), string.Empty);
					}
				}
			}
			while (this.designCode.IndexOf("\r\n\r\n") > -1)
			{
				this.designCode = this.designCode.Replace("\r\n\r\n", "\r\n");
			}
			this.ViewPageDesign.CurrentPage.Custom = this.designCode;
			this.ViewPageDesign.PageService.VSW_Core_CPSave(this.ViewPageDesign.CurrentPage);
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			string[] allKeys = this.ViewPageDesign.CurrentTemplate.Items.AllKeys;
			for (int i = 0; i < allKeys.Length; i++)
			{
				this._item[allKeys[i]] = this.ViewPageDesign.CurrentTemplate.Items.GetValue(allKeys[i]);
			}
			this.designCode = this.ViewPageDesign.CurrentTemplate.Custom;
			if (this.ViewPageDesign.CurrentPage != null)
			{
				allKeys = this.ViewPageDesign.CurrentPage.Items.AllKeys;
				for (int j = 0; j < allKeys.Length; j++)
				{
					if (!this._item.Exists(allKeys[j]))
					{
						object obj = this.designCode;
						object[] args = new object[]
						{
							obj,
							"\r\n",
							allKeys[j],
							"=",
							this.ViewPageDesign.CurrentPage.Items.GetValue(allKeys[j])
						};
						this.designCode = string.Concat(args);
					}
					else
					{
						string text = this.designCode;
						string str = allKeys[j];
						string str2 = "=";
						VSW.Core.Global.Object value = this._item.GetValue(allKeys[j]);
						string oldValue = str + str2 + ((value != null) ? value.ToString() : null);
						string str3 = allKeys[j];
						string str4 = "=";
						VSW.Core.Global.Object value2 = this.ViewPageDesign.CurrentPage.Items.GetValue(allKeys[j]);
						this.designCode = text.Replace(oldValue, str3 + str4 + ((value2 != null) ? value2.ToString() : null));
					}
					this._item[allKeys[j]] = this.ViewPageDesign.CurrentPage.Items.GetValue(allKeys[j]);
				}
			}
			if (this.Code != "VSWMODULE")
			{
				this.CurrentModule = this.ViewPageDesign.ModuleService.VSW_Core_GetByCode(this.Code);
			}
			if (this.CurrentModule != null)
			{
				this.CurrentObject = new Class(this.CurrentModule.ModuleType);
			}
			else if (this.Code != "VSWMODULE")
			{
				throw new Exception("Module/Control : '" + this.Code + "'' not found.");
			}
			if (this.CphName == string.Empty)
			{
				this.AloneMode = true;
			}
			if (this.Code == "VSWMODULE")
			{
				this.ReadOnlyMode = true;
			}
			else if (this.CurrentModule == null)
			{
				this.ErrorMode = true;
			}
			if (this.Page.IsPostBack)
			{
				string text2 = HttpForm.GetValue("vsw_submit").ToString();
				if (text2 == this.ViewControlID + "|save")
				{
					this.method_3();
					return;
				}
				if (text2 == this.ViewControlID + "|up")
				{
					this.method_0();
					return;
				}
				if (text2 == this.ViewControlID + "|down")
				{
					this.method_1();
					return;
				}
				if (text2 == this.ViewControlID + "|delete")
				{
					this.method_4();
					return;
				}
				if (text2 == "vsw_cph_" + this.CphName + "|add")
				{
					this.method_2(this.CphName, HttpForm.GetValue("vsw_cph_" + this.CphName + "_control_code").ToString(), HttpForm.GetValue("vsw_cph_" + this.CphName + "_VSWID").ToString());
					return;
				}
				if (text2.StartsWith("vsw_cph_") && text2.EndsWith("|add") && !this.ViewPageDesign.Boolean_0)
				{
					int num = text2.IndexOf("vsw_cph_");
					int num2 = text2.IndexOf("|add");
					if (num2 > num)
					{
						string text3 = text2.Substring(7, num2 - 7);
						if (text3 != string.Empty)
						{
							this.ViewPageDesign.Boolean_0 = true;
							this.method_2(text3, HttpForm.GetValue("vsw_cph_" + text3 + "_control_code").ToString(), HttpForm.GetValue("vsw_cph_" + text3 + "_VSWID").ToString());
							return;
						}
					}
				}
				else if (text2.EndsWith("|move") && !this.ViewPageDesign.Boolean_1)
				{
					this.ViewPageDesign.Boolean_1 = true;
					text2 = text2.Replace("|move", string.Empty);
					if (text2.StartsWith("to_cph_"))
					{
						text2 = text2.Replace("to_cph_", string.Empty);
						char[] separator = new char[]
						{
							'$'
						};
						if (text2.Split(separator).Length == 3)
						{
							char[] separator2 = new char[]
							{
								'$'
							};
							string text4 = text2.Split(separator2)[0];
							char[] separator3 = new char[]
							{
								'$'
							};
							string text5 = text2.Split(separator3)[1];
							char[] separator4 = new char[]
							{
								'$'
							};
							string text6 = text2.Split(separator4)[2];
							string text7 = string.Empty;
							string text8 = this._item.GetValue("Template_" + text6).ToString();
							char[] separator5 = new char[]
							{
								','
							};
							string[] array = text8.Split(separator5);
							int num3 = System.Array.IndexOf<string>(array, text5);
							int num4 = -1;
							string[] array2 = new string[array.Length - 1];
							for (int k = 0; k < array.Length; k++)
							{
								if (k != num3)
								{
									num4++;
									array2[num4] = array[k];
								}
							}
							this.designCode = this.designCode.Replace("Template_" + text6 + "=" + text8, "Template_" + text6 + "=" + VSW.Core.Global.Array.ToString(array2));
							text7 = VSW.Core.Global.Array.ToString(array2);
							if (!this._item.Exists("Template_" + text4))
							{
								string[] values = new string[]
								{
									"Template_",
									text4,
									"=",
									text5,
									"\r\n",
									this.designCode
								};
								this.designCode = string.Concat(values);
							}
							else
							{
								string text9 = string.Empty;
								text9 = ((text6 != text4) ? this._item.GetValue("Template_" + text4).ToString() : text7);
								char[] separator6 = new char[]
								{
									','
								};
								string[] array3 = text9.Split(separator6);
								if (System.Array.IndexOf<string>(array3, text5) > -1)
								{
									return;
								}
								System.Array.Resize<string>(ref array3, array3.Length + 1);
								array3[array3.Length - 1] = text5;
								this.designCode = this.designCode.Replace("Template_" + text4 + "=" + text9, "Template_" + text4 + "=" + VSW.Core.Global.Array.ToString(array3));
							}
							this.method_5();
							base.Response.Redirect(base.Request.RawUrl);
							return;
						}
					}
					else if (text2.StartsWith("to_hlid_"))
					{
						text2 = text2.Replace("to_hlid_", string.Empty);
						char[] separator7 = new char[]
						{
							'$'
						};
						if (text2.Split(separator7).Length == 4)
						{
							char[] separator8 = new char[]
							{
								'$'
							};
							string text10 = text2.Split(separator8)[0];
							char[] separator9 = new char[]
							{
								'$'
							};
							string text11 = text2.Split(separator9)[1];
							char[] separator10 = new char[]
							{
								'$'
							};
							string text12 = text2.Split(separator10)[2];
							char[] separator11 = new char[]
							{
								'$'
							};
							string text13 = text2.Split(separator11)[3];
							if (text10 != text12)
							{
								string text14 = string.Empty;
								string text15 = this._item.GetValue("Template_" + text13).ToString();
								char[] separator12 = new char[]
								{
									','
								};
								string[] array4 = text15.Split(separator12);
								int num5 = System.Array.IndexOf<string>(array4, text12);
								int num6 = -1;
								string[] array5 = new string[array4.Length - 1];
								for (int l = 0; l < array4.Length; l++)
								{
									if (l != num5)
									{
										num6++;
										array5[num6] = array4[l];
									}
								}
								this.designCode = this.designCode.Replace("Template_" + text13 + "=" + text15, "Template_" + text13 + "=" + VSW.Core.Global.Array.ToString(array5));
								text14 = VSW.Core.Global.Array.ToString(array5);
								string text16 = string.Empty;
								text16 = ((text13 != text11) ? this._item.GetValue("Template_" + text11).ToString() : text14);
								char[] separator13 = new char[]
								{
									','
								};
								string[] array6 = text16.Split(separator13);
								int num7 = System.Array.IndexOf<string>(array6, text10);
								if (num7 > -1)
								{
									string[] array7 = new string[array6.Length + 1];
									array7[num7] = text12;
									int num8 = -1;
									for (int m = 0; m < array6.Length; m++)
									{
										num8++;
										if (m == num7)
										{
											num8++;
										}
										array7[num8] = array6[m];
									}
									this.designCode = this.designCode.Replace("Template_" + text11 + "=" + text16, "Template_" + text11 + "=" + VSW.Core.Global.Array.ToString(array7));
									this.method_5();
									base.Response.Redirect(base.Request.RawUrl);
								}
							}
						}
					}
				}
			}
		}
		public bool ReadOnlyMode;	
		public bool AloneMode;
		public bool ErrorMode;
		private Custom _item = new Custom();
		private string designCode = string.Empty;
	}
}
