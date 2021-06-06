using System;
using System.Collections.Generic;
using System.Linq;
using tcc_back.SystemExceptions;

namespace tcc_back.util
{
    public static class Validation
    {
        public static void emptyResultList<T>(List<T> list)
        {
            if (!list.Any())
                throw new NotFoundException();
        }

        public static void emptyParameter(string str)
        {
            if (String.IsNullOrEmpty(str))
                throw new InvalidParameterException();
        }

        public static void emptyResult(string str)
        {
            if (String.IsNullOrEmpty(str))
                throw new NotFoundException();
        }

    }

}