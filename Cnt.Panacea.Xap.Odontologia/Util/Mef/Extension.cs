using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Hosting;
using System.Threading.Tasks;


public static class OperationExtensions
{
    public static Task<T> DownloadAsyncasTask<T>(this T operation)   where T : DeploymentCatalog
    {
        TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();

        operation.DownloadCompleted += (sender, e) =>
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send("Cargado", "Mef");     

            if (e.Error != null)
            {
                tcs.TrySetException(e.Error);                
            }            
            else
            {
                tcs.TrySetResult(operation);
            }
        };
        operation.DownloadAsync();
        return tcs.Task;
    }

}



