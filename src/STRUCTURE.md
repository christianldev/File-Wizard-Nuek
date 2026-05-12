# Estructura de Proyecto - File Wizard

## Principios de Clean Code

El proyecto ha sido reorganizado siguiendo los principios de **Clean Code** y **SOLID**:

### Estructura de Carpetas

```
File Wizard/
├── src/
│   ├── Program.cs                          # Punto de entrada de la aplicación
│   ├── UI/
│   │   ├── Forms/
│   │   │   ├── MainMenuForm.cs
│   │   │   ├── MainMenuForm.Designer.cs
│   │   │   └── MainMenuForm.resx
│   │   ├── DesaTest/
│   │   │   ├── DesaTestDownloadForm.cs
│   │   │   ├── DesaTestDownloadForm.Designer.cs
│   │   │   ├── DesaTestDownloadForm.resx
│   │   │   ├── DesaTestUploadForm.cs
│   │   │   ├── DesaTestUploadForm.Designer.cs
│   │   │   └── DesaTestUploadForm.resx
│   │   └── Qa/
│   │       ├── QaDownloadForm.cs
│   │       ├── QaDownloadForm.Designer.cs
│   │       └── QaDownloadForm.resx
│   └── Infrastructure/
│       └── Bootstrap/
│           └── WinFormsBootstrapper.cs     # Configuración de Windows Forms
├── Properties/
├── Resources/
└── File Wizard.csproj

```

## Organización por Capas

### **src/UI/Forms**

- **Responsabilidad**: Formularios de entrada principal
- **Contenido**:
  - `MainMenuForm`: Pantalla principal con opciones de navegación

### **src/UI/DesaTest**

- **Responsabilidad**: Funcionalidad específica de DESA-TEST
- **Contenido**:
  - `DesaTestDownloadForm`: Descarga de archivos DESA-TEST
  - `DesaTestUploadForm`: Carga de archivos DESA-TEST

### **src/UI/Qa**

- **Responsabilidad**: Funcionalidad específica de QA
- **Contenido**:
  - `QaDownloadForm`: Descarga de archivos QA

### **src/Infrastructure/Bootstrap**

- **Responsabilidad**: Configuración e inicialización del framework
- **Contenido**:
  - `WinFormsBootstrapper`: Configuración de temas y DPI de Windows Forms

## Principios Aplicados

### 1. **Separación de Responsabilidades**

Cada carpeta/forma tiene una responsabilidad clara y específica

### 2. **Namespaces Significativos**

Los namespaces reflejan la estructura del proyecto:

- `File_Wizard.UI.Forms` → Formularios principales
- `File_Wizard.UI.DesaTest` → Lógica DESA-TEST
- `File_Wizard.UI.Qa` → Lógica QA
- `File_Wizard.Infrastructure.Bootstrap` → Inicialización

### 3. **Escalabilidad**

Fácil de agregar nuevas funcionalidades:

- Nuevas formas en `UI/`
- Nuevos módulos en `Infrastructure/`
- Lógica compartida puede extraerse a una carpeta `Core/`

### 4. **Mantenibilidad**

Código más fácil de encontrar, entender y modificar

## Mejoras Futuras Sugeridas

1. **Crear carpeta `Core/`** para lógica compartida (conexiones SSH, validaciones)
2. **Crear carpeta `Services/`** para servicios de negocio
3. **Patrones de Inyección de Dependencias** para mayor desacoplamiento
4. **Tests Unitarios** con estructura parallel bajo `Tests/`

---

**Última actualización**: 2026-05-11
