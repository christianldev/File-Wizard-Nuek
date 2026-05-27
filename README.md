# File-Wizard-Nuek

File Wizard es una aplicación de escritorio desarrollada en .NET 10 y WPF que permite gestionar transferencias de archivos entre distintos entornos, con modos de descarga y subida, selección de ambientes y opciones manuales o asistidas.

## Configuracion SFTP por ambiente

La ventana de login carga Host y Puerto de forma automatica segun el entorno seleccionado (DESA, TEST, QA) leyendo variables desde `.env`.

### Variables requeridas en `.env`

```env
SFTP_DESA_HOST=...
SFTP_DESA_PORT=22

SFTP_TEST_HOST=...
SFTP_TEST_PORT=22

SFTP_QA_HOST=...
SFTP_QA_PORT=22
```

### Inicializacion rapida

1. Crear `.env` local copiando `.env.example`.
2. Reemplazar hosts y puertos con valores reales de cada ambiente.
3. Abrir la app y seleccionar el ambiente en el login para autocompletar Host y Puerto.

Si falta una clave o el puerto no es valido, la ventana mostrara una advertencia y permitira completar los datos manualmente.
