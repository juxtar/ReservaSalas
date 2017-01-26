# Capa de Acceso a Datos

Este assembly contendrá todo lo relacionado al acceso a datos. El mismo se hace mediante Entity Framework a una base de datos relacional.

Requiere una referencia al assembly que contiene las clases de modelo que serán almacenadas, el [Modelo de Dominio](../../../tree/master/ReservaSalas).

## Consideraciones

De manera de simplificar la implementación de almacén de usuarios y todo lo relacionado a autenticación, el contexto de la base de datos está definido en el assembly de [Web Api](../../../tree/master/WebApi).

Se decidió esto para no tener que utilizar dos contextos distintos, lo que requiere un manejo más minucioso de las migraciones.
