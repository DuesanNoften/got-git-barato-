
using System.IO;

void help()
{
    Console.WriteLine("\n   got init <name> : Inicia el repositorio con el nombre indicado");//Parcialmente implementado
    Console.WriteLine("\n   got help : Muestra las funciones disponibles y qué realizan");//Implementado
    Console.WriteLine("\n   got Add [-A] [name] : Si utiliza -A agreagara todos los archivos modificados o nuevos\n   tambien puede agregar archivos manualmente al ingresar el nombre ");//Parcialmente implementado
    Console.WriteLine("\n   got commit <mensaje> : Envia los archivos al server. Debe ingresarse un mensaje cada \n   vez que lo realice");//Parcialmente implementado
    Console.WriteLine("\n   got status <file> : Muestra los archivos que han sido modificados o agregados, si se \n   especifica un archivo se mostrara su historial de cambios");//Por implementar
    Console.WriteLine("\n   got rollback <file> <commit> : Permite regresar un archivo a su version en un commit \n   especifico");//Por implementar
    Console.WriteLine("\n   got reset <file> : Deshace los cambios realizados en un archivo y lo devuelve al ultimo\n   commit");//Por implementar
    Console.WriteLine("\n   got sync <file> : Recupera los cambios de un archivo en el server y lo sincroniza con \n   el archivo en el cliente. Si hay algún cambio debe realizarse un merge");//Por implementar
}

/*
 * Fucion que crea las configuraciones pertinentes, auxiliar para init
 * E: string
 */
void config(string name)
{
    string path = Directory.GetCurrentDirectory();
    path += "\\.init";
    Console.WriteLine(path);

    if (!Directory.Exists(path))
    {
        Console.WriteLine("I got in");
        try
        {
            Directory.CreateDirectory(path);
            System.IO.File.Create(path+ "\\config.txt");
            File.Create(path + "\\changed.txt");
            File.Create(path+"\\commit.txt");
        }
        catch
        {

        }
    } 
    
    
}

//Funcion que crea los archivos necesarios para utilizar el programa
//E: string que indica el nombre del repositorio
void init(string name)
{
    string path = Directory.GetCurrentDirectory();
    path += "\\.init";
    if (!Directory.Exists(path))
    {
        config(name);
    }
    else
    {
        Console.WriteLine("Ya existe un repositorio en esta carpeta");
    }
}

/*
 * Función encargada de revisar si un archivo ya ha sido agregado a un documento
 * E: string con la ruta del archivo
 * S: bool que indica si el archivo ya ha sido agregado o no
 */
bool lineExist(string file, string path)
{
    bool exist=false;
    string[] lines = File.ReadAllLines(path);
    foreach(string line in lines)
    {
        if (line == file) 
        {
            exist = true;
            break;
        }
    }
    return exist;
}

/*
 * Funcion encargada de añadir archivos al documento de manejo de cambios
 * E: Un string
 * S: Void
 */
void add(string file)
{
    string path = Directory.GetCurrentDirectory();
    path += "\\.init\\changed.txt";
    //Si se ingresa -A se añaden todos los archivos que hay en la carpeta
    if (file == "-A")
    {
        String[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
        try
        {
            if (File.Exists(path))
            {
                System.IO.File.WriteAllLines(path, files);
            }
        }
        catch
        {

        }
    }
    //En caso contrario se trata de agregar solo el archivo solicitado
    else
    {
        if (File.Exists(Directory.GetCurrentDirectory() + "\\" + file))
        {
            //Se verifica que el archivo no haya sido agregado anteriormente
            if (!lineExist(Directory.GetCurrentDirectory() + "\\" + file,path))
            {
                File.AppendAllText(path, Directory.GetCurrentDirectory() + "\\" + file + "\n");
            }
            else
            {
                Console.WriteLine("El archivo ya esta siendo manejado");
            }
        }           
        else
            {
            Console.WriteLine("El archivo especificado no existe");
            }
    }
}

void commit(string message)
{
    string path = Directory.GetCurrentDirectory();
    string changepath = path + "\\.init\\changed.txt";
    string commpath =path+"\\.init\\commit.txt";
    string[] commited =new string[2];
    commited[0] = message;
    commited[1] = changepath;
    File.WriteAllLines(commpath, commited);

}



help();
init("hola");
add("Hola.txt");
commit("Se añadio el commit");