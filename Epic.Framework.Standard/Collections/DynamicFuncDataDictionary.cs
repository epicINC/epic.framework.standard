using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Epic.Collections
{


    public delegate TResult GetMemberFunc<T1, T2, out TResult>(T1 arg1, out T2 arg2);

    public sealed class DynamicFuncDataDictionary<T> : DynamicObject
    {
        public DynamicFuncDataDictionary(Func<T> thunk,
            GetMemberFunc<GetMemberBinder, object, bool> getMember,
            Func<SetMemberBinder, object, bool> setMember
            )
        {
            this.thunk = thunk;
            this.GetMember = getMember;
            this.SetMember = setMember;
        }

        readonly Func<T> thunk;

        T Data
        {
            get { return this.thunk(); }
        }

        GetMemberFunc<GetMemberBinder, object, bool> GetMember
        {
            get;
            set;
        }

        Func<SetMemberBinder, object, bool> SetMember
        {
            get;
            set;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return this.GetMember(binder, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return this.SetMember(binder, value);
        }

    }




}
