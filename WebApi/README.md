# API Backend para el cliente Angular

API REST con ASP.NET Web Api, implementando autenticación con ASP.NET Identity.

Contiene referencias a los assembly de [Modelo de Dominio](../../../tree/master/ReservaSalas) y al de [Lógica de Acceso a Datos](../../../tree/master/DataAccess), si bien se decidió implementar el contexto de la base de datos en este assembly.

## Configuración

Debe configurar la dirección del [cliente Angular](../../../tree/master/Angular), debido a restricciones de cors, en el archivo [App_Start/WebApiConfig.cs](App_Start/WebApiConfig.cs) de la siguiente manera:
```cs
public static void Register(HttpConfiguration config)
{
    // Configuración y servicios de API web
    var cors = new EnableCorsAttribute("[Dirección cliente]", "*", "*");
    config.EnableCors(cors);

    /* Otras configuraciones */
}
```
