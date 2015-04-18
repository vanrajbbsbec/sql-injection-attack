using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace SQLIA
{

    class QueryParser
    {

        private string[] tokenizeSQLSmt(string pSQLSmt)
        {
            return pSQLSmt.Split(' ');
        }

        private ArrayList parseTokenToInt(ArrayList arrSQLTokens)
        {
            ArrayList arrTokenAsInt = new ArrayList();
            for (int i = 0; i < arrSQLTokens.Count; i++)
            {
                int tokenAsInt = 0;

                byte[] asciiByte = Encoding.ASCII.GetBytes(arrSQLTokens[i].ToString());
                int[] bytesAsInts = Array.ConvertAll(asciiByte, c => (int)c);

                for (int j = 0; j < bytesAsInts.Length; j++)
                {
                    bytesAsInts[j] = bytesAsInts[j] * (j + 1);
                    tokenAsInt += bytesAsInts[j];
                }

                arrTokenAsInt.Add(tokenAsInt);
            }
            return arrTokenAsInt;
        }


        public void Display(List<MainList> pSQLDBArrList)
        {
            int i = 0;
            foreach (MainList mListObject in pSQLDBArrList)
            {
                Console.Write("-->{0} Tokens: {1}\t", mListObject.GetType().Name, i+1);
                mListObject.ListNodes();
                Console.WriteLine(Environment.NewLine);
                i++;
            }
        }

        public SingleLinkedList AddTokensToList(ArrayList arr, SingleLinkedList pListOject)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                pListOject.AddAtEnd(Convert.ToInt32(arr[i]));
            }
            return pListOject;
        }

        public void ParseSQLSmt(string pSQLStatement, SingleLinkedList pListOject, List<MainList> pSQLDBArr)
        {
            ArrayList arrTokenizedList = new ArrayList();

            string[] tokenizedList = tokenizeSQLSmt(pSQLStatement);
            arrTokenizedList.AddRange(tokenizedList);
            
            ArrayList arrIntegerTokens = parseTokenToInt(arrTokenizedList);

            if(pSQLDBArr.ElementAtOrDefault(arrIntegerTokens.Count) == null)
            {
                for (int i = 0; i < arrIntegerTokens.Count - 1; i++)
                {
                    if (pSQLDBArr.ElementAtOrDefault(i) == null)
                    {
                        pSQLDBArr.Add(new MainList());
                    }
                }
                MainList mList = new MainList();
                pSQLDBArr.Add(mList);
            }
            
            AddSingleListToMainList(AddTokensToList(arrIntegerTokens, pListOject), pSQLDBArr[arrIntegerTokens.Count - 1]);
        }

        public void AddSingleListToMainList(SingleLinkedList pListOject, MainList pMainListObject)
        {
            //Do something here
            pMainListObject.AddAtEnd(pListOject);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Intializing SQL Inject Attack Detection engine..");
            List<MainList> SQLDBArr = new List<MainList>();
            QueryParser objParser = new QueryParser();
            DBFunctions dbfunction = new DBFunctions();
            DataTable dtValid = dbfunction.GetValidSQLSmt();
            for (int i = 0; i < dtValid.Rows.Count; i++)
            {
                SingleLinkedList sList = new SingleLinkedList();
                objParser.ParseSQLSmt(dtValid.Rows[i][0].ToString().ToLower(), sList, SQLDBArr);
                //Console.WriteLine(Environment.NewLine);
                //sList.ListNodes();
            }
            Console.WriteLine("Completed.");
            Console.WriteLine(Environment.NewLine);
            objParser.Display(SQLDBArr);
            Console.ReadLine();
        }
    }
}
