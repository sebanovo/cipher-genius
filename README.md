# Proyecto de demostración de métodos de cifrado

<a href="https://postimages.org/" target="_blank"><img src="https://i.postimg.cc/mr7Zztzk/Captura-de-pantalla-2024-06-06-144502.png" alt="Captura-de-pantalla-2024-06-06-144502"/></a> <a href="https://postimages.org/" target="_blank"><img src="https://i.postimg.cc/wvV62Zkd/Captura-de-pantalla-2024-06-06-144804.png" alt="Captura-de-pantalla-2024-06-06-144804"/></a> <a href="https://postimages.org/" target="_blank"><img src="https://i.postimg.cc/d1vs8wrB/Captura-de-pantalla-2024-06-06-144855.png" alt="Captura-de-pantalla-2024-06-06-144855"/></a> <a href="https://postimages.org/" target="_blank"><img src="https://i.postimg.cc/vTZGNhPx/Captura-de-pantalla-2024-06-06-144923.png" alt="Captura-de-pantalla-2024-06-06-144923"/></a><br/><br/>
<a href="https://postimages.org/" target="_blank"><img src="https://i.postimg.cc/HW6YyGxw/Captura-de-pantalla-2024-06-06-144950.png" alt="Captura-de-pantalla-2024-06-06-144950"/></a> <a href="https://postimages.org/" target="_blank"><img src="https://i.postimg.cc/fyqMndF2/Captura-de-pantalla-2024-06-06-145140.png" alt="Captura-de-pantalla-2024-06-06-145140"/></a> 

## Métodos de cifrado implementados

### 1. Transposición por grupos

el método de transposición por grupos consiste en reorganizar los caracteres del mensaje
original en grupos de cierto tamaño. esto cambia el orden de los caracteres y puede
dificultar la lectura del mensaje para quienes no conocen la clave de cifrado, ejemplo.

```c#
// cifrar:
string mensaje = "MI PERRO SE LLAMA GUANTE";
int posicion = 5;
string permutacion = "4 3 2 1 0";

// descifrar:
string criptograma = "REPIMLESORGAMALETNAU";
int posicion = 5;
string permutacion = "4 3 2 1 0";
```

### 2. Cifrador de doble transposición por columnas

El cifrador de doble transposición por columnas implica aplicar dos etapas de
transposición por columnas al mensaje original. primero, se reorganizan los caracteres en
columnas según una clave de cifrado. luego, se reorganizan nuevamente las columnas para
aumentar la seguridad del cifrado, ejemplo.

```c#
// cifrar:
string mensaje = "EL EXITO CONSISTE EN VENCER EL TEMOR AL FRACASO";
string clave = "IMAGEN";

// descifrar:
string criptograma = "SCEOROELNENIXCETOXXAREALEENIOLSFMRVATCESTX";
string clave = "IMAGEN";
```

### 3. Transposición por filas

La transposición por filas implica reorganizar los caracteres del mensaje original en
filas en lugar de columnas. este método cambia el orden de los caracteres y puede
dificultar la lectura del mensaje para quienes no tienen la clave de cifrado, ejemplo.

```c#
// cifrar:
string mensaje = "ALGORITMO POR FILAS";
string clave = "CARO";

// descifrar:
string criptograma = "LIPIXAROFSOMRAXGTOLX";
string clave = "CARO";
```
## Uso del proyecto

El proyecto proporciona implementaciones de los métodos de cifrado mencionados
anteriormente. cada método se encuentra en un archivo separado dentro del proyecto. para
utilizar los métodos de cifrado, sigue las instrucciones en cada archivo de código fuente.
