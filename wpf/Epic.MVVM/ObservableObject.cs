
using Epic.Expressions;
using Epic.FluentAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Epic.MVVM
{
    public class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region Events

        public event PropertyChangingEventHandler PropertyChanging;


        public virtual void RaisePropertyChanging([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanging == null) return;
            this.PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }

        public virtual void RaisePropertyChanging<T>(Expression<Func<T>> propertyExpression)
        {
            this.RaisePropertyChanging(ExpressionHelper.PropertyName(propertyExpression));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged == null) return;
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            this.RaisePropertyChanged(ExpressionHelper.PropertyName(propertyExpression));
        }

        #endregion

        protected bool Set<T>(string propertyName, ref T field, T value)
        {
            if (Object.Equals(field, value)) return false;
            this.RaisePropertyChanging(propertyName);
            field = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T value)
        {
            return this.Set<T>(SimpleAccess.Property(propertyExpression)?.Name ?? throw new ArgumentException("Argument is not a property", "propertyExpression"), ref field, value);
        }

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            return this.Set<T>(propertyName, ref field, value);
        }

        bool Set(object target, PropertyInfo property, object value)
        {
            if (Object.Equals(property.GetValue(target), value)) return false;
            this.RaisePropertyChanging(property.Name);
            property.SetValue(target, value);
            this.RaisePropertyChanged(property.Name);
            return true;
            
        }

        protected bool Set<T, K>(T target, Expression<Func<T, K>> propertyExpression, K value)
        {
            var p = SimpleAccess.Property(propertyExpression) ?? throw new ArgumentException("Argument is not a property", "propertyExpression");
            return this.Set(target, p, value);
        }



    }
}
