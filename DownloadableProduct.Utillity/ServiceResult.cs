using System.Collections.Generic;

namespace DownloadableProduct.Utillity
{
    public class ServiceResult
    {
        public ServiceResult() { }
        public ServiceResult(bool success)
        {
            Success = success;
        }
        public bool Success { get; set; }
        public List<ErrorResult> Errors { get; set; } = new List<ErrorResult>();

        public static ServiceResult Okay()
        {
            return new ServiceResult
            {
                Success = true
            };
        }

        public static ServiceResult Error(string key)
        {
            return new ServiceResult
            {
                Success = false,
                Errors = new List<ErrorResult>
                {
                    new ErrorResult
                    {
                    Code = key
                    }
                }
            };
        }

        public static ServiceResult Error()
        {
            return new ServiceResult
            {
                Success = false,
                Errors = new List<ErrorResult>
                {
                    new ErrorResult
                    {
                    Code = "Error"
                    }
                }
            };
        }
        public void AddError(string key)
        {
            Success = false;
            Errors.Add(new ErrorResult
            {
                Code = key
            });
        }
    }
    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult(bool success)
        {
            Success = success;
        }
        public ServiceResult(bool success,T data)
        {
            Success = success;
            Data = data;
        }
        public T Data { get; set; }
    }
}
