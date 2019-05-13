using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;

namespace CodeSample.ExpressionTree
{
    public class Tree
    {
        /// <summary>
        /// 对象转换
        /// return p => new t(){ }
        /// </summary>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <returns></returns>
        public static TDestination Convert<TSource,TDestination>(TSource source) where TSource:class,new() where TDestination:class,new()
        {
            var sourceType = typeof(TSource);
            var para = Expression.Parameter(sourceType, "p");
            var sourceProperties = typeof(TSource).GetProperties();
            List<MemberBinding> memberBindingList = new List<MemberBinding>();
            foreach (var property in typeof(TDestination).GetProperties())
            {
                if (!property.CanWrite || !sourceProperties.Any(p => p.Name == property.Name))
                {
                    continue;
                }
                MemberExpression memberExpression = Expression.Property(para, property.Name); //p.[property]
                MemberBinding memberBinding = Expression.Bind(property, memberExpression);//property = p.[property]
                memberBindingList.Add(memberBinding);
            }
            var newExpression = Expression.New(typeof(TDestination));// new t(); 成员初始化子节点
            var memberInitExpression = Expression.MemberInit(newExpression, memberBindingList); // new t(){ memberInit } //子节点
            Expression<Func<TSource, TDestination>> converter = Expression.Lambda<Func<TSource, TDestination>>(memberInitExpression, new ParameterExpression[] { para }); //p => new t(){} 目标表达式
            return converter.Compile()(source);
        }
    }
}
