namespace Markey.CrossCutting.ResponseData;

public class ResponseData<TData, TMessage>
{
    public int Code { get; set; } 
    public TData Data { get; set; }
    public List<string> Messages { get; set; }

    public ResponseData(int code, TData data, List<string> message)
    {
        Code = code;
        Data = data;
        Messages = message;
    }
}
