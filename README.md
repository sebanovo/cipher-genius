# proyecto de demostración de métodos de cifrado

este proyecto tiene como objetivo demostrar diferentes métodos de cifrado utilizados en la
criptografía. los métodos de cifrado implementados son:

1. transposición por grupos
2. cifrador de doble transposición por columnas
3. transposición por filas

[![cipher-genius.png](https://i.postimg.cc/fbN0rxt1/cipher-genius.png)](https://postimg.cc/Yvd94Lxx)

## métodos de cifrado implementados

### 1. transposición por grupos

el método de transposición por grupos consiste en reorganizar los caracteres del mensaje
original en grupos de cierto tamaño. esto cambia el orden de los caracteres y puede
dificultar la lectura del mensaje para quienes no conocen la clave de cifrado, ejemplo.

```c#
// cifrar:
string mensaje = "mi perro se llama guante";
int posicion = 5;
string permutacion = "4 3 2 1 0";

// descifrar:
string criptograma = "repimlesorgamaletnau";
int posicion = 5;
string permutacion = "4 3 2 1 0";
```

### 2. cifrador de doble transposición por columnas

el cifrador de doble transposición por columnas implica aplicar dos etapas de
transposición por columnas al mensaje original. primero, se reorganizan los caracteres en
columnas según una clave de cifrado. luego, se reorganizan nuevamente las columnas para
aumentar la seguridad del cifrado, ejemplo.

```c#
// cifrar:
string mensaje = "el exito consiste en vencer el temor al fracaso";
string clave = "imagen";

// descifrar:
string criptograma = "sceoroelnenixcetoxxarealeeniolsfmrvatcestx";
string clave = "imagen";
```

### 3. transposición por filas

la transposición por filas implica reorganizar los caracteres del mensaje original en
filas en lugar de columnas. este método cambia el orden de los caracteres y puede
dificultar la lectura del mensaje para quienes no tienen la clave de cifrado, ejemplo.

```c#
// cifrar:
string mensaje = "algoritmo por filas";
string clave = "caro";

// descifrar:
string criptograma = "lipixarofsomraxgtolx";
string clave = "caro";
```
## uso del proyecto

el proyecto proporciona implementaciones de los métodos de cifrado mencionados
anteriormente. cada método se encuentra en un archivo separado dentro del proyecto. para
utilizar los métodos de cifrado, sigue las instrucciones en cada archivo de código fuente.

## ejemplo de implementación

```csharp
namespace criptographer {
    class ciphermanager
    {
        public string primercifrado(string mensaje, string clave, ref datagridview datagridview1){}
        public string segundocifrado(string cifradointermedio, string clave, ref datagridview datagridview1, ref datagridview datagridview2){}
        public string cifrarporfilas(string mensaje, string clave, ref datagridview datagridview1, ref datagridview datagridview2){}
        public string cifrarporgrupos(string mensaje, int p, int[] permutacion){}
    }
}
```
