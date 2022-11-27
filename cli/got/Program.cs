using System.IO;
using System.Text;
using System.Text.Json;

void Help()
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
void Config(string name)
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
            File.Create(path+ "\\config.txt");
            File.Create(path + "\\changed.txt");
            File.Create(path+"\\commit.txt");
            File.Create(path + "\\roll.txt");
        }
        catch
        {
            // ignored
        }
    } 
    
    
}

//Funcion que crea los archivos necesarios para utilizar el programa
//E: string que indica el nombre del repositorio
async void Init(string name)
{
    string path = Directory.GetCurrentDirectory();
    path += "\\.init";
    if (!Directory.Exists(path))
    {
        Config(name);
    }
    else
    {
        Console.WriteLine("Ya existe un repositorio en esta carpeta");
    }
    var json= JsonSerializer.Serialize(name);
    var data=new StringContent(json,Encoding.UTF8);
    var url="http://localhost:7030/post";
    try
    {
        using var client = new HttpClient();
        var response= await client.PostAsync(url,data);
        string result=response.Content.ReadAsStringAsync().Result;
        Console.WriteLine(result);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }

}

/*
 * Función encargada de revisar si un archivo ya ha sido agregado a un documento
 * E: string con la ruta del archivo
 * S: bool que indica si el archivo ya ha sido agregado o no
 */
bool LineExist(string file, string path)
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
void Add(string file)
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
                File.WriteAllLines(path, files);
            }
        }
        catch
        {
            // ignored
        }
    }
    //En caso contrario se trata de agregar solo el archivo solicitado
    else
    {
        if (File.Exists(Directory.GetCurrentDirectory() + "\\" + file))
        {
            //Se verifica que el archivo no haya sido agregado anteriormente
            if (!LineExist(Directory.GetCurrentDirectory() + "\\" + file,path))
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

void Commit(string message)
{
    string path = Directory.GetCurrentDirectory();
    string changepath = path + "\\.init\\changed.txt";
    string commpath =path+"\\.init\\commit.txt";
    string[] commited =new string[2];
    commited[0] = message;
    commited[1] = changepath;
    File.WriteAllLines(commpath, commited);

}

void Status()
{
    //Aquí revisa el commit anterior y devuelve la  respuesta
}

/*
*Funcion encargada de volver a la version de un archivo de un commit anterior
*E: string file (nombre del archivo a cambiar), string commit (El id del commit a buscar)
*/
void Rollback(string file, string commit)
{
    string path=Directory.GetCurrentDirectory()+"\\.init\\roll.txt";
    string[] rolling=new string[2];
    rolling[0]=file;
    rolling[1]=commit;
    File.WriteAllLines(path,rolling);
}

void Reset()
{
    string path = Directory.GetCurrentDirectory() + "\\.init\\changed.txt";
    string[] delete = new string[2];
    delete[0] = "";
    delete[1] = "";
    File.WriteAllLines(path, delete);
}

async void Sync(string file)
{
    using var client =new HttpClient();
    var content=await client.GetStringAsync("http://localhost:7030/post");
    Console.WriteLine(content);
}

Help();
Init("hola");
Add("Hola.txt");
Commit("Se añadio el commit");
Rollback("Hola.txt", "0");
Reset();
Sync("Hola.txt");
