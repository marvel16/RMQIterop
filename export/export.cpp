// export.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "export.h"


// This is an example of an exported variable
EXPORT_API int nexport=0;

// This is an example of an exported function.
EXPORT_API int fnexport(void)
{
    return 42;
}

// This is the constructor of a class that has been exported.
// see export.h for the class definition
Cexport::Cexport()
{
    return;
}
