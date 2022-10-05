using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sovtech_HM.Utilities
{
    public class ApplicationResponseCode
    {
        public static ErrorMessage LoadErrorMessageByCode(string Code)
        {

            try
            {

                var dataResult = ErrorMessageDataBank().ToList();
                return dataResult.Where(a => a.Code == Code).FirstOrDefault();

            }
            catch (Exception ex)
            {
                var mess = new ErrorMessage
                {
                    Code = "Unknowing",
                    Name = ex.Message
                };
                return mess;
            }
        }



        public class ErrorMessage
        {
            public string Name { get; set; }
            public string Code { get; set; }

        }


        private static List<ErrorMessage> ErrorMessageDataBank()
        {
            var listData = new List<ErrorMessage>();

            var data = new ErrorMessage();


            try
            {

                var data01 = new ErrorMessage
                {
                    Code = "1000",
                    Name = "Inner Error"
                };
                listData.Add(data01);

                var data200 = new ErrorMessage
                {
                    Code = "200",
                    Name = "Successfully"
                };
                listData.Add(data200);

                var data500 = new ErrorMessage
                {
                    Code = "500",
                    Name = "Internal Server Error"
                };
                listData.Add(data500);

                var data404 = new ErrorMessage
                {
                    Code = "404",
                    Name = "No Matching Record Found"
                };
                listData.Add(data404);

                return listData.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
