using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class Path_Servicio
{    

    public static string obtenerUrlServicio()
    {
        //return "http://localhost:3481/api/";
        return "http://hefesoftdynamicbackend.azurewebsites.net/api/";
    }

    public static string obtenerUrlServicioPdf()
    {
        //return "http://localhost:11274/api/";
        return "http://hefesoftpdfendpoint.azurewebsites.net//api/";
    }
}

