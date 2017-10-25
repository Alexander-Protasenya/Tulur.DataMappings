﻿using System;
using System.Linq.Expressions;
using FsMapper.Extensions;

namespace FsMapper.Build
{
    public class ExpressionCtorObjectBuilder : IObjectBuilder
    {
        public Expression<Func<TDest>> GetActivator<TDest>()
        {
            var ctor = typeof(TDest).GetDefaultConstructor();
            if (ctor == null) throw new MissingMemberException(string.Format(Resources.MissingDefaultConstructor, typeof(TDest).Name));
            return Expression.Lambda<Func<TDest>>(Expression.New(ctor));
        }
    }
}