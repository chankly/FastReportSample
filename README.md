# FastReportSample
## Creación de informes con .NET
Hay tres proyectos importantes:  
1. PerformanceReport.Api  
   Es el que genera on-fly el informe y se lo devuelve al usuario.
3. PerformanceReport.Creator  
   Se usa para crear una estructura básica inicial del informe con un DataSource asignado y así poder editarlo con el editor de FastReport.
   En este caso, se crea un informe asociando un modelo `new List<PostTeamMatchReportModel>()`.
5. PerformanceReport.ImageCreator  
   Se usa para poder probar la creación de imágenes mediante código. Se usa para probar y crear funciones que devuelvan imágens que se quieren usar en el informe, una vez creadas y probadas, se podrían pasar al proyecto principal, que es el que realmente va a generar el informe.

## Diseñador  
Aunque es descargable desde la web de FastReport, en el repositorio se adjunta una versión del diseñador FastReport.Community.
