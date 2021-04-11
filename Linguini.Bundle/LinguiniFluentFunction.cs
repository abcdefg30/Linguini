﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Text;
using Linguini.Bundle.Types;
using Linguini.Syntax.Ast;

namespace Linguini.Bundle
{
    public static class LinguiniFluentFunctions
    {
        public static IFluentType Number(IList<IFluentType> args, IDictionary<string, IFluentType> namedArgs)
        {
            var num = args[0].ToFluentNumber();
            if (num != null)
            {
                // TODO merge named arguments
                return num;
            }

            return new FluentErrType();
        }

        public static IFluentType Sum(IList<IFluentType> args, IDictionary<string, IFluentType> namedArgs)
        {
            var sum = 0.0;
            for (var i = 0; i < args.Count; i++)
            {
                var fluentType = args[i].ToFluentNumber();
                if (fluentType == null)
                {
                    return new FluentErrType();
                }

                sum += fluentType.Value;
            }

            return (FluentNumber) sum;
        }

        public static IFluentType Identity(IList<IFluentType> args, IDictionary<string, IFluentType> namedArgs)
        {
            try
            {
                var x = args[0];
                return x;
            }
            catch (Exception e)
            {
                return new FluentErrType();
            }
        }

        public static IFluentType Concat(IList<IFluentType> args, IDictionary<string, IFluentType> namedArgs)
        {
            var stringConcat = new StringBuilder();
            for (var i = 0; i < args.Count; i++)
            {
                var str = args[i];
                if (str.TryConvert(out FluentString fs))
                {
                    stringConcat.Append(fs);
                }
                else if (str.TryConvert(out FluentNumber fn))
                {
                    stringConcat.Append(fn);
                }
            }

            return (FluentString) stringConcat.ToString();
        }
    }
}