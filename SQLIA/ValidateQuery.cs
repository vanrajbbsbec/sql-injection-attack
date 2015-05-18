using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace SQLIA
{
    class ValidateQuery : QueryParser
    {
        public Boolean Validator(string pUserQuery, List<MainList> pSQLDBArr)
        {
            Boolean IsQueryValid = false;
            ArrayList arrTokenizedList = new ArrayList();
            string[] tokenizedUserQuery = tokenizeSQLSmt(pUserQuery);
            arrTokenizedList.AddRange(tokenizedUserQuery);

            ArrayList arrIntegerTokens = parseTokenToInt(arrTokenizedList);
            MainList objMainList = pSQLDBArr[arrIntegerTokens.Count - 1];
            IsQueryValid = objMainList.Compare(arrIntegerTokens);
            return IsQueryValid;
        }
    }
}
