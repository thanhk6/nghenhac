using System;
using System.Reflection;
using VSW.Core.Global;

namespace VSW.Core.Models
{
	
	public abstract class EntityBase
	{
		
		public virtual int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}
		public object this[string propertyName]
		{
			get
			{
				return this.Module.GetProperty(propertyName);
			}
			set
			{
				this.Module.SetProperty(propertyName, value);
			}
		}

		public Custom Items
		{
			get
			{
				if (this._item == null)
				{
					this._item = new Custom();
				}
				return this._item;
			}
		}
		private Class Module
		{
			get
			{
				if (this._module == null)
				{
					this._module = new Class(this);
				}
				return this._module;
			}
		}
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		public EntityBase Clone()
		{
			Class @class = new Class(base.GetType());
			foreach (PropertyInfo propertyInfo in @class.GetPropertiesInfo())
			{
				if (propertyInfo.CanRead && (propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType == typeof(string) || propertyInfo.PropertyType == typeof(DateTime)))
				{
					try
					{
						if (propertyInfo.CanWrite)
						{
							@class.SetProperty(propertyInfo.Name, this.Module.GetProperty(propertyInfo.Name));
						}
					}
					catch
					{
					}
				}
			}
			return (EntityBase)@class.Instance;
		}

		private Class _module;
		private Custom _item;
		private int _id;
		private string _name;
	}
}
