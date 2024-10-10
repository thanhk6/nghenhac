using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using VSW.Core.Global;
using VSW.Core.Models;
namespace VSW.Core.MVC
{
    public class Controller
    {
        public virtual string DefaultAction
        {
            get
            {
                return this._defaultAction;
            }
            set
            {
                this._defaultAction = value;

                if (!string.IsNullOrEmpty(this._defaultAction) && !this._defaultAction.StartsWith("Action"))
                {
                    this._defaultAction = "Action" + this._defaultAction;
                }
            }
        }
        public virtual string IndexAction
        {
            get
            {
                return this._indexAction;
            }
            set
            {
                this._indexAction = value;
                if (!string.IsNullOrEmpty(this._indexAction) && !this._indexAction.StartsWith("Action"))
                {
                    this._indexAction = "Action" + this._indexAction;
                }
            }
        }
        public Dictionary<string, object> SetObject { get; protected set; }
        public dynamic ViewBag { get; protected set; }

        public ViewControl ViewControl { get; internal set; }

        public Dictionary<string, object> ViewData { get; protected set; }
        public virtual string ViewLayout
        {
            get
            {
                return this._viewLayout;
            }
            set
            {
                this._viewLayout = value;
            }
        }

        public ViewPageBase ViewPageBase { get; private set; }
        public Controller()
        {
            this.ViewData = new Dictionary<string, object>();
            this.SetObject = new Dictionary<string, object>();
            this.ViewBag = new DynamicObject(this.ViewData);
        }
        public virtual void OnLoad()
        {
        }
        public virtual void OnUnLoad()
        {
            foreach (string key in this.SetObject.Keys)
            {
                this.ViewControl.SetObject(key, this.SetObject[key]);
            }
        }

        public object GetObject(string key)
        {
            return this.ViewControl.GetObject(key);
        }
        internal void InitPage(ViewPageBase viewPage)
        {
            this.ViewPageBase = viewPage;
        }
        internal void SetCustom(Custom custom)
        {
            Class @class = new Class(this);
            string[] allKeys = custom.AllKeys;
            for (int i = 0; i < allKeys.Length; i++)
            {
                if (@class.ExistsProperty(allKeys[i]))
                {
                    //PropertyInfo propertyInfo = @class.GetPropertyInfo(allKeys[i]);
                    //@class.SetProperty(allKeys[i], VSW.Core.Global.Convert.AutoValue(custom.GetValue(allKeys[i]).Current, propertyInfo.PropertyType));
                    var propertyInfo = @class.GetPropertyInfo(allKeys[i]);

                    @class.SetProperty(allKeys[i], Global.Convert.AutoValue(custom.GetValue(allKeys[i]).Current, propertyInfo.PropertyType));
                }
                else if (@class.ExistsField(allKeys[i]))
                {
                    FieldInfo fieldInfo = @class.GetFieldInfo(allKeys[i]);
                    @class.SetField(allKeys[i], VSW.Core.Global.Convert.AutoValue(custom.GetValue(allKeys[i]).Current, fieldInfo.FieldType));
                }
            }
        }
        public void RedirectToAction(string action)
        {
            if (!string.IsNullOrEmpty(action))
            {
                if (!action.StartsWith("Action"))
                {
                    action = "Action" + action;
                }
                Class @class = new Class(this);
                ViewPageRender.RenderPage(this.ViewPageBase, this, @class, action);
            }
        }

        public void RenderView(string viewLayout)
        {
            this.ViewLayout = viewLayout;
        }
        public void TryUpdateModel<T>(T model)
        {
            this.TryUpdateModel<T>(model, string.Empty, null, null);
        }
        public void TryUpdateModel<T>(T model, Expression<Func<T, object>> propertyExec)
        {
            this.TryUpdateModel<T>(model, string.Empty, propertyExec, null);
        }
        public void TryUpdateModel<T>(T model, string prefix)
        {
            this.TryUpdateModel<T>(model, prefix, null, null);
        }
        public void TryUpdateModel<T>(T model, Expression<Func<T, object>> propertyExec, Expression<Func<T, object>> propertyNotExec)
        {
            this.TryUpdateModel<T>(model, string.Empty, propertyExec, propertyNotExec);
        }
        public void TryUpdateModel<T>(T model, string prefix, Expression<Func<T, object>> propertyExec)
        {
            this.TryUpdateModel<T>(model, prefix, propertyExec, null);
        }
        public void TryUpdateModel<T>(T model, string prefix, Expression<Func<T, object>> propertyExec, Expression<Func<T, object>> propertyNotExec)
        {
            string[] propertyExec2 = null;
            string[] propertyNotExec2 = null;
            if (propertyExec != null)
            {
                propertyExec2 = new DBToLinQ<T>().Select(propertyExec).Replace("[", string.Empty).Replace("]", string.Empty).Split(new char[]
                {
                    ','
                });
            }
            if (propertyNotExec != null)
            {
                propertyNotExec2 = new DBToLinQ<T>().Select(propertyNotExec).Replace("[", string.Empty).Replace("]", string.Empty).Split(new char[]
                {
                    ','
                });
            }
            this.TryUpdateModel(model, prefix, propertyExec2, propertyNotExec2);
        }

        public void TryUpdateModel(object model, string prefix, string[] propertyExec, string[] propertyNotExec)
        {
            var class22 = new Class22
            {
                string_0 = prefix
            };
            var class2 = new Class(model);
            var class21 = new Class21();
            var infoArray = class2.GetPropertiesInfo();
            int index = 0;
            Label_0028:
            if (index >= infoArray.Length)
            {
                return;
            }
            class21.propertyInfo = infoArray[index];
            if (class21.propertyInfo.CanWrite)
            {
                if (ViewPageBase.PageViewState.Exists(class22.string_0 + class21.propertyInfo.Name))
                {
                    if (((propertyExec != null) && (System.Array.IndexOf<string>(propertyExec, class21.propertyInfo.Name) == -1)) || ((propertyNotExec != null) && (System.Array.IndexOf<string>(propertyNotExec, class21.propertyInfo.Name) > -1)))
                    {
                        goto Label_06CA;
                    }
                    if (class21.propertyInfo.PropertyType == typeof(int[]))
                    {
                        class2.SetProperty(class21.propertyInfo.Name, ViewPageBase.PageViewState.GetValue(class22.string_0 + class21.propertyInfo.Name).ToInts());
                        goto Label_06CA;
                    }
                    if (class21.propertyInfo.PropertyType == typeof(long[]))
                    {
                        class2.SetProperty(class21.propertyInfo.Name, ViewPageBase.PageViewState.GetValue(class22.string_0 + class21.propertyInfo.Name).ToLongs());
                        goto Label_06CA;
                    }
                    if (class21.propertyInfo.PropertyType == typeof(double[]))
                    {
                        class2.SetProperty(class21.propertyInfo.Name, ViewPageBase.PageViewState.GetValue(class22.string_0 + class21.propertyInfo.Name).ToDoubles());
                        goto Label_06CA;
                    }
                    if (class21.propertyInfo.PropertyType == typeof(decimal[]))
                    {
                        class2.SetProperty(class21.propertyInfo.Name, ViewPageBase.PageViewState.GetValue(class22.string_0 + class21.propertyInfo.Name).ToDecimals());
                        goto Label_06CA;
                    }
                    if (class21.propertyInfo.PropertyType == typeof(bool[]))
                    {
                        class2.SetProperty(class21.propertyInfo.Name, ViewPageBase.PageViewState.GetValue(class22.string_0 + class21.propertyInfo.Name).ToBools());
                        goto Label_06CA;
                    }
                    if (class21.propertyInfo.PropertyType == typeof(DateTime[]))
                    {
                        class2.SetProperty(class21.propertyInfo.Name, ViewPageBase.PageViewState.GetValue(class22.string_0 + class21.propertyInfo.Name).ToDateTimes());
                        goto Label_06CA;
                    }
                    if (class21.propertyInfo.PropertyType == typeof(string[]))
                    {
                        class2.SetProperty(class21.propertyInfo.Name, ViewPageBase.PageViewState.GetValue(class22.string_0 + class21.propertyInfo.Name).ToStrings());
                        goto Label_06CA;
                    }
                    if (class21.propertyInfo.PropertyType == typeof(HttpPostedFile))
                    {
                        class2.SetProperty(class21.propertyInfo.Name, ViewPageBase.Request.Files[class22.string_0 + class21.propertyInfo.Name]);
                        goto Label_06CA;
                    }
                    if ((class21.propertyInfo.PropertyType.IsValueType || (class21.propertyInfo.PropertyType == typeof(string))) || (class21.propertyInfo.PropertyType == typeof(DateTime)))
                    {
                        try
                        {
                            class2.SetProperty(class21.propertyInfo.Name, Global.Convert.AutoValue(ViewPageBase.PageViewState[class22.string_0 + class21.propertyInfo.Name], class21.propertyInfo.PropertyType));
                        }
                        catch
                        {
                        }
                        goto Label_06CA;
                    }
                }
                if ((!class21.propertyInfo.PropertyType.IsValueType && (class21.propertyInfo.PropertyType != typeof(string))) && (class21.propertyInfo.PropertyType != typeof(DateTime)))
                {
                    try
                    {
                        if (class21.propertyInfo.PropertyType.IsGenericType)
                        {
                            Type type = class21.propertyInfo.PropertyType.GetGenericArguments()[0];
                            if ((!type.IsValueType && (type != typeof(string))) && (type != typeof(DateTime)))
                            {
                                Type type2 = typeof(List<>);
                                object propertyValue = Activator.CreateInstance(type2.MakeGenericType(class21.propertyInfo.PropertyType.GetGenericArguments()));
                                Class class3 = new Class(propertyValue);
                                List<string> list = new List<string>();
                                list.AddRange(ViewPageBase.PageViewState.AllKeys);
                                Predicate<string> match = null;
                                Class23 class23 = new Class23
                                {
                                    class21_0 = class21,
                                    class22_0 = class22,
                                    Index = 0
                                };
                                while (class23.Index < list.Count)
                                {
                                    if (match == null)
                                    {
                                        match = new Predicate<string>(class23.Exists);
                                    }
                                    if (list.Find(match) != null)
                                    {
                                        object obj3 = Activator.CreateInstance(type);
                                        TryUpdateModel(obj3, string.Concat(new object[] { class22.string_0, class21.propertyInfo.Name, "[", class23.Index, "]." }));
                                        class3.CallMethod("Add", new object[] { obj3 });
                                    }
                                    class23.Index++;
                                }
                                class2.SetProperty(class21.propertyInfo.Name, propertyValue);
                            }
                        }
                        else if (class21.propertyInfo.Name != "Item")
                        {
                            object propertyValue = Activator.CreateInstance(class21.propertyInfo.PropertyType);
                            TryUpdateModel(propertyValue, class22.string_0 + class21.propertyInfo.Name + ".");
                            class2.SetProperty(class21.propertyInfo.Name, propertyValue);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            Label_06CA:
            index++;
            goto Label_0028;
        }

        //public void TryUpdateModel(object model, string prefix, string[] propertyExec, string[] propertyNotExec)
        //{

        //    var class22 = new Class22
        //    {
        //        string_0 = prefix
        //    };
        //    var class2 = new Class(model);
        //    var class21 = new Class21();
        //    var infoArray = class2.GetPropertiesInfo();
        //    int index = 0;
        //Label_0028:

        //    if (index >= infoArray.Length)
        //    {
        //        return;
        //    }
        //    class21.propertyInfo = infoArray[index];

        //    for (int i = 0; i < propertiesInfo.Length; i++)
        //    {
        //        class3.propertyInfo = propertiesInfo[i];
        //        if (class3.propertyInfo.CanWrite)
        //        {
        //            if (this.ViewPageBase.PageViewState.Exists(@class.string_0 + class3.propertyInfo.Name))
        //            {
        //                if ((propertyExec != null && System.Array.IndexOf<string>(propertyExec, class3.propertyInfo.Name) == -1) || (propertyNotExec != null && System.Array.IndexOf<string>(propertyNotExec, class3.propertyInfo.Name) > -1))
        //                {
        //                    goto IL_682;
        //                }
        //                if (class3.propertyInfo.PropertyType == typeof(int[]))
        //                {
        //                    class2.SetProperty(class3.propertyInfo.Name, this.ViewPageBase.PageViewState.GetValue(@class.string_0 + class3.propertyInfo.Name).ToInts());
        //                    goto IL_682;
        //                }
        //                if (class3.propertyInfo.PropertyType == typeof(long[]))
        //                {
        //                    class2.SetProperty(class3.propertyInfo.Name, this.ViewPageBase.PageViewState.GetValue(@class.string_0 + class3.propertyInfo.Name).ToLongs());
        //                    goto IL_682;
        //                }
        //                if (class3.propertyInfo.PropertyType == typeof(double[]))
        //                {
        //                    class2.SetProperty(class3.propertyInfo.Name, this.ViewPageBase.PageViewState.GetValue(@class.string_0 + class3.propertyInfo.Name).ToDoubles());
        //                    goto IL_682;
        //                }
        //                if (class3.propertyInfo.PropertyType == typeof(decimal[]))
        //                {
        //                    class2.SetProperty(class3.propertyInfo.Name, this.ViewPageBase.PageViewState.GetValue(@class.string_0 + class3.propertyInfo.Name).ToDecimals());
        //                    goto IL_682;
        //                }
        //                if (class3.propertyInfo.PropertyType == typeof(bool[]))
        //                {
        //                    class2.SetProperty(class3.propertyInfo.Name, this.ViewPageBase.PageViewState.GetValue(@class.string_0 + class3.propertyInfo.Name).ToBools());
        //                    goto IL_682;
        //                }
        //                if (class3.propertyInfo.PropertyType == typeof(DateTime[]))
        //                {
        //                    class2.SetProperty(class3.propertyInfo.Name, this.ViewPageBase.PageViewState.GetValue(@class.string_0 + class3.propertyInfo.Name).ToDateTimes());
        //                    goto IL_682;
        //                }
        //                if (class3.propertyInfo.PropertyType == typeof(string[]))
        //                {
        //                    class2.SetProperty(class3.propertyInfo.Name, this.ViewPageBase.PageViewState.GetValue(@class.string_0 + class3.propertyInfo.Name).ToStrings());
        //                    goto IL_682;
        //                }
        //                if (class3.propertyInfo.PropertyType == typeof(HttpPostedFile))
        //                {
        //                    class2.SetProperty(class3.propertyInfo.Name, this.ViewPageBase.Request.Files[@class.string_0 + class3.propertyInfo.Name]);
        //                    goto IL_682;
        //                }
        //                if (class3.propertyInfo.PropertyType.IsValueType || class3.propertyInfo.PropertyType == typeof(string) || class3.propertyInfo.PropertyType == typeof(DateTime))
        //                {
        //                    try
        //                    {
        //                        class2.SetProperty(class3.propertyInfo.Name, VSW.Core.Global.Convert.AutoValue(this.ViewPageBase.PageViewState[@class.string_0 + class3.propertyInfo.Name], class3.propertyInfo.PropertyType));
        //                        goto IL_682;
        //                    }
        //                    catch
        //                    {
        //                        goto IL_682;
        //                    }
        //                }
        //            }
        //            if (!class3.propertyInfo.PropertyType.IsValueType && class3.propertyInfo.PropertyType != typeof(string) && class3.propertyInfo.PropertyType != typeof(DateTime))
        //            {
        //                try
        //                {
        //                    if (class3.propertyInfo.PropertyType.IsGenericType)
        //                    {
        //                        Type type = class3.propertyInfo.PropertyType.GetGenericArguments()[0];
        //                        if (!type.IsValueType && type != typeof(string) && type != typeof(DateTime))
        //                        {
        //                            object obj = Activator.CreateInstance(typeof(List<>).MakeGenericType(class3.propertyInfo.PropertyType.GetGenericArguments()));
        //                            Class class4 = new Class(obj);
        //                            List<string> list = new List<string>();
        //                            list.AddRange(this.ViewPageBase.PageViewState.AllKeys);
        //                            Predicate<string> predicate = null;
        //                            Controller.Class23 class5 = new Controller.Class23
        //                            {
        //                                class21_0 = class3,
        //                                class22_0 = @class,
        //                                Index = 0
        //                            };
        //                            while (class5.Index < list.Count)
        //                            {
        //                                if (predicate == null)
        //                                {
        //                                    predicate = new Predicate<string>(class5.Exists);
        //                                }
        //                                if (list.Find(predicate) != null)
        //                                {
        //                                    object obj2 = Activator.CreateInstance(type);
        //                                    this.TryUpdateModel<object>(obj2, string.Concat(new object[]
        //                                    {
        //                                        @class.string_0,
        //                                        class3.propertyInfo.Name,
        //                                        "[",
        //                                        class5.Index,
        //                                        "]."
        //                                    }));
        //                                    class4.CallMethod("Add", new object[]
        //                                    {
        //                                        obj2
        //                                    });
        //                                }
        //                                class5.Index++;
        //                            }
        //                            class2.SetProperty(class3.propertyInfo.Name, obj);
        //                        }
        //                    }
        //                    else if (class3.propertyInfo.Name != "Item")
        //                    {
        //                        object obj3 = Activator.CreateInstance(class3.propertyInfo.PropertyType);
        //                        this.TryUpdateModel<object>(obj3, @class.string_0 + class3.propertyInfo.Name + ".");
        //                        class2.SetProperty(class3.propertyInfo.Name, obj3);
        //                    }
        //                }
        //                catch
        //                {
        //                }
        //            }
        //        }
        //    IL_682:;
        //    }
        //}
        public string UpdateFormByJS<T>(T model)
        {
            return this.UpdateFormByJS<T>(model, string.Empty, null, null);
        }
        public string UpdateFormByJS(string[] propertyExec)
        {
            return this.UpdateFormByJS(string.Empty, propertyExec, null);
        }
        public string UpdateFormByJS<T>(T model, Expression<Func<T, object>> propertyExec)
        {
            return this.UpdateFormByJS<T>(model, string.Empty, propertyExec, null);
        }
        public string UpdateFormByJS(string prefix, string[] propertyExec)
        {
            return this.UpdateFormByJS(prefix, propertyExec, null);
        }
        public string UpdateFormByJS(string[] propertyExec, string[] propertyNotExec)
        {
            return this.UpdateFormByJS(string.Empty, propertyExec, propertyNotExec);
        }
        public string UpdateFormByJS<T>(T model, string prefix)
        {
            return this.UpdateFormByJS<T>(model, prefix, null, null);
        }
        public string UpdateFormByJS<T>(T model, string prefix, Expression<Func<T, object>> propertyExec)
        {
            return this.UpdateFormByJS<T>(model, prefix, propertyExec, null);
        }
        public string UpdateFormByJS(string prefix, string[] propertyExec, string[] propertyNotExec)
        {
            string text = string.Empty;
            string[] allKeys = this.ViewPageBase.PageViewState.AllKeys;
            int num = 0;
            while (allKeys != null)
            {
                if (num >= allKeys.Length)
                {
                    return text;
                }
                string text2 = allKeys[num];
                if (!text2.StartsWith("_vsw_action") && (prefix == string.Empty || text2.StartsWith(prefix)) && (propertyExec == null || System.Array.IndexOf<string>(propertyExec, text2) != -1) && (propertyNotExec == null || System.Array.IndexOf<string>(propertyNotExec, text2) <= -1))
                {
                    string source = this.ViewPageBase.PageViewState.GetValue(text2).ToString();
                    string text3 = text;
                    text = string.Concat(new string[]
                    {
                        text3,
                        "control_set_value('",
                        text2,
                        "','",
                        Support.EscapeQuote(source),
                        "');\r\n"
                    });
                }
                num++;
            }
            return text;
        }

        public string UpdateFormByJS<T>(T model, Expression<Func<T, object>> propertyExec, Expression<Func<T, object>> propertyNotExec)
        {
            return this.UpdateFormByJS<T>(model, string.Empty, propertyExec, propertyNotExec);
        }
        public string UpdateFormByJS<T>(T model, string prefix, Expression<Func<T, object>> propertyExec, Expression<Func<T, object>> propertyNotExec)
        {
            string[] array = null;
            string[] array2 = null;
            if (propertyExec != null)
            {
                array = new DBToLinQ<T>().Select(propertyExec).Replace("[", string.Empty).Replace("]", string.Empty).Split(new char[]
                {
                    ','
                });
            }
            if (propertyNotExec != null)
            {
                array2 = new DBToLinQ<T>().Select(propertyNotExec).Replace("[", string.Empty).Replace("]", string.Empty).Split(new char[]
                {
                    ','
                });
            }

            Class @class = new Class(model);
            string text = string.Empty;

            foreach (System.Reflection.PropertyInfo propertyInfo in @class.GetPropertiesInfo())
            {
                if ((array == null || System.Array.IndexOf<string>(array, propertyInfo.Name) != -1) && (array2 == null || System.Array.IndexOf<string>(array2, propertyInfo.Name) <= -1) && propertyInfo.CanRead && (propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType == typeof(string) || propertyInfo.PropertyType == typeof(DateTime)))
                {
                    try
                    {
                        object property = @class.GetProperty(propertyInfo.Name);
                        if (property != null)
                        {
                            string text2 = text;
                            text = string.Concat(new string[]
                            {
                                text2,
                                "control_set_value('",
                                prefix,
                                propertyInfo.Name,
                                "','",
                                Support.EscapeQuote(property.ToString()),
                                "');\r\n"
                            });
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return text;
        }

        private string _indexAction = "ActionIndex";
        private string _defaultAction = "ActionDetail";


        private string _viewLayout;
        private sealed class Class21
        {

            //public PropertyInfo propertyInfo;
            public System.Reflection.PropertyInfo propertyInfo;
        }
        private sealed class Class22
        {

            public string string_0;
        }
        private sealed class Class23
        {
            public bool Exists(string key)
            {
                return key.StartsWith(string.Concat(new object[]
                {
                    this.class22_0.string_0,
                    this.class21_0.propertyInfo.Name,
                    "[",
                    this.Index,
                    "]."
                }));
            }
            public Controller.Class21 class21_0;


            public Controller.Class22 class22_0;


            public int Index;
        }
    }
}
