﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using VSW.Core.Web;
using VSW.Lib.Models;

namespace VSW.Lib.Global
{
    public class CartItem
    {
        public int ProductID { get; set; }
        //public int PackingID { get; set; }
        public int GiftID { get; set; }
        public int Quantity { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is CartItem)) return base.Equals(obj);

            var temp = (CartItem)obj;

            //return ProductID.Equals(temp.ProductID) && PackingID.Equals(temp.PackingID);
            return ProductID.Equals(temp.ProductID);
        }

        public override int GetHashCode()
        {
            //return (ProductID + "-" + PackingID).GetHashCode();
            return (ProductID).GetHashCode();
        }
    }

    public class Cart
    {
        private readonly List<CartItem> _listItem = new List<CartItem>();

        private readonly string _cookieKey = "VSW_Cart";

        public ReadOnlyCollection<CartItem> Items => _listItem.AsReadOnly();

        public int Count => _listItem.Count;

        public Cart() : this(string.Empty) { }

        public Cart(string serviceName)
        {
            _cookieKey += serviceName;

            if (ObjectStorage<List<CartItem>>.Exists(_cookieKey))
                _listItem = ObjectStorage<List<CartItem>>.GetValue(_cookieKey);

            if (_listItem == null)
                _listItem = new List<CartItem>();
        }


        public bool Exists(CartItem item)
        {
            return _listItem.Contains(item);
        }


        public void Add(CartItem item)
        {
            Remove(item);

            _listItem.Add(item);
        }

        public CartItem Find(CartItem item)
        {
            return _listItem.Find(o => o.Equals(item));
        }

        public void Remove(CartItem item)
        {
            if (Exists(item))
                _listItem.Remove(item);
        }

        public void RemoveAll()
        {
            _listItem.Clear();
        }

        public void Save()
        {
            if (_listItem.Count > 0)


                ObjectStorage<List<CartItem>>.SetValue(_cookieKey, _listItem);
            else
                ObjectStorage<List<CartItem>>.Remove(_cookieKey);
        }

        public List<ModProductEntity> GetProduct()
        {
            var list = new List<ModProductEntity>();
            for (int i = 0; i < _listItem.Count; i++)
            {
                var product = ModProductService.Instance.GetByID(_listItem[i].ProductID);

                if (product == null) continue;

                list.Add(product);
            }

            return list;
        }
    }
}