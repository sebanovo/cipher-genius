# Proyecto de demostración de métodos de cifrado

<table>
  <tr>
    <td>
      <p>Adolfo Mendoza Ribera</p>
      <a href="https://postimages.org/" target="_blank">
        <img src="https://i.postimg.cc/dVk9FVG1/Captura-de-pantalla-2024-06-08-172010.png" alt="Captura-de-pantalla-2024-06-08-172010"/>
      </a>
    </td>
    <td>
      <p>Mejia Zarate Alimbert</p>
      <a href="https://postimages.org/" target="_blank"><img src="https://i.postimg.cc/76j0ZSkW/Captura-de-pantalla-2024-06-08-172236.png" alt="Captura-de-pantalla-2024-06-08-172236"/></a>
    </td>
  </tr>
  <tr>
    <td>
      <p>Yo</p>
      <a href="https://postimages.org/" target="_blank">
        <img src="https://i.postimg.cc/5NkmDCYT/Captura-de-pantalla-2024-06-08-172144.png" alt="Captura-de-pantalla-2024-06-08-172144"/>
      </a>
      </td>
    <td>
      <p>Yo</p>
      <a href="https://postimages.org/" target="_blank"><img src="https://i.postimg.cc/90TTRzXF/Captura-de-pantalla-2024-06-08-172214.png" alt="Captura-de-pantalla-2024-06-08-172214"/></a>
    </td>
  </tr>
  <tr>
    <td>
      <p>Josué Vito Zarate Mollo</p>
    <a href="https://postimages.org/" target="_blank"><img src="https://i.postimg.cc/MGP1ghrx/Captura-de-pantalla-2024-06-08-17223121.png" alt="Captura-de-pantalla-2024-06-08-17223121"/></a><br/><br/>
    <td></td>
  </tr>
</table>

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

### 4. Vigenere

Es un método de cifrado por sustitución polialfabética que utiliza una palabra clave
repetida para determinar el desplazamiento de cada letra en el texto claro. Cada letra de
la palabra clave indica el número de posiciones que se debe desplazar la letra
correspondiente del texto claro en el alfabeto.

```c#
// cifrar:
string mensaje = "CIFRADO VIGENERE";
string clave = "PERRO";

// descifrar:
string criptograma = "RMWJOSSNZUTQVJS";
string clave = "PERRO";
```

### 5. Homofono

Es un tipo de cifrado por sustitución que asigna múltiples símbolos (homófonos) a cada
letra del alfabeto para hacer más difícil el análisis de frecuencia. Cada letra del texto
claro puede ser sustituida por cualquiera de sus símbolos asignados, aumentando así la
complejidad del cifrado.

```c#
// cifrar:
string mensaje = "MARIA NO TE AMA";
string clave = "PATO";

// descifrar:
string criptograma = "69, 58, 74, 66, 58, 70, 71, 51, 62, 58, 69, 58";
string clave = "PATO";
```

### 6. Playfair

El cifrado Playfair es un método de cifrado por sustitución digráfica que utiliza una
matriz de 5x5 para cifrar pares de letras (dígrafos). Cada letra del mensaje se reemplaza
según su posición en la matriz, siguiendo reglas específicas para intercambiar posiciones
y asegurar que ninguna letra se repita.

```c#
// cifrar:
string mensaje = "HOLA BEBE";
string clave = "MESSI";

// descifrar:
string criptograma = "KHOSCMCM";
string clave = "MESSI";
```

### 7. Hill

El cifrado Hill es un cifrado por sustitución poligráfica basado en álgebra lineal.
Utiliza matrices y operaciones matriciales para transformar bloques de letras del texto
claro en texto cifrado. La clave del cifrado es una matriz invertible que se usa para
multiplicar bloques de letras y producir el texto cifrado.

```c#
// cifrar:
string mensaje = "";
string clave = "";

// descifrar:
string criptograma = "";
string clave = "";
```

### 8. Beufort

El cifrado Beaufort es un método de cifrado por sustitución polialfabética similar al
cifrado Vigenère, pero con una diferencia clave en su método de cifrado y descifrado.
Utiliza una tabla de Beaufort (una variante de la tabla de Vigenère) para cifrar el texto
claro. Cada letra del texto claro se cifra usando una clave repetitiva, desplazando las
letras en sentido inverso al cifrado Vigenère.

```c#
// cifrar:
string mensaje = "QUE SE RINDA SU ABUELA";
string clave = "BOLIVAR";

// descifrar:
string criptograma = "PGTLJRQMOPLZAKTPAS";
string clave = "BOLIVAR";
```

### 9. Transformación por conversión de base

La transformación por conversión de base es un proceso matemático que cambia un número de
una base numérica a otra. Por ejemplo, convertir un número del sistema decimal (base 10)
al sistema binario (base 2) o hexadecimal (base 16).

```c#
// cifrar:
string mensaje = "";
string clave = "";

// descifrar:
string criptograma = "";
string clave = "";
```

### 10. Transformación por lógica de Boole

La transformación por lógica de Boole utiliza operaciones lógicas básicas (AND, OR, NOT,
XOR) sobre los valores binarios (0 y 1) para manipular y procesar datos. Es fundamental en
circuitos digitales y programación.

```c#
// cifrar:
string mensaje = "";
string clave = "";

// descifrar:
string criptograma = "";
string clave = "";
```

### 11. Transformación matricial

La transformación matricial emplea matrices y operaciones matriciales para modificar
datos. Se utiliza ampliamente en gráficos por computadora, álgebra lineal y cifrado de
datos, donde los datos se representan y transforman mediante multiplicaciones y adiciones
de matrices.

```c#
// cifrar:
string mensaje = "";
string clave = "";

// descifrar:
string criptograma = "";
string clave = "";
```

### 12. Francmason

El cifrado Francmason, también conocido como cifrado Pigpen, es un cifrado por sustitución
simple que reemplaza cada letra del alfabeto con un símbolo gráfico. Estos símbolos se
derivan de una cuadrícula o rejilla predefinida, dividiendo el alfabeto en segmentos. Es
conocido por su apariencia visual distintiva y fue utilizado históricamente por los
masones para mantener sus escritos encriptados.

# pendiente:

- Francmson
- Hill
- Transformación por conversión de base
- Transformación por lógica de Boole
- Transformación matricial

## Uso del proyecto

El proyecto proporciona implementaciones de los métodos de cifrado mencionados
anteriormente. cada método se encuentra en un archivo separado dentro del proyecto. para
utilizar los métodos de cifrado, sigue las instrucciones en cada archivo de código fuente.
