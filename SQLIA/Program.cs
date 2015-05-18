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

        public string[] tokenizeSQLSmt(string pSQLSmt)
        {
            return pSQLSmt.Split(' ');
        }

        public ArrayList parseTokenToInt(ArrayList arrSQLTokens)
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
                Console.Write("-->{0} Tokens: {1}\t", mListObject.GetType().Name, i + 1);
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

            if (pSQLDBArr.ElementAtOrDefault(arrIntegerTokens.Count) == null)
            {
                for (int i = 0; i < arrIntegerTokens.Count - 1; i++)
                {
                    if (pSQLDBArr.ElementAtOrDefault(i) == null)
                    {
                        pSQLDBArr.Add(new MainList()); // Add empty doubleLinkedList before the actual index,
                        //we are adding a scrap value of doubleLinkedList just to move further to actuall index
                    }
                }
                MainList mList = new MainList();
                pSQLDBArr.Add(mList); //Adding the actual doubleLinkedList, Correct Index will be reached due to the FOR loop above.
            }

            AddSingleListToMainList(AddTokensToList(arrIntegerTokens, pListOject), pSQLDBArr[arrIntegerTokens.Count - 1]);
        }

        public void AddSingleListToMainList(SingleLinkedList pListOject, MainList pMainListObject)
        {
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

            ValidateQuery objValidateQuery = new ValidateQuery();

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

            string choice = string.Empty;
            string userQuery = string.Empty;
            while(true)
            {
                Console.WriteLine("\n************* 1. Validate Query *****************");
                Console.WriteLine("************* 2. EXIT ***************************");
                Console.Write("Enter Choice : ");
                choice = Console.ReadLine();
                if (choice.Length > 1)
                {
                    continue;
                }
                else
                {
                    switch (Convert.ToInt32(choice))
                    {
                        case 1: Console.WriteLine("\nEnter SQL query to check :");
                            userQuery = Console.ReadLine();
                            if (objValidateQuery.Validator(userQuery.ToLower(), SQLDBArr))
                            {
                                objParser.Display(SQLDBArr);
                                Console.WriteLine("\nQuery Valid.\n");
                            }
                            else
                            {
                                Console.WriteLine("\nWarning : Detected SQL Injection Attack!\n");
                            }
                            break;

                        case 2: Environment.Exit(1);
                            break;
                    }
                }
            }
        }
    }
}
