////////// DECLARACION DE VARIABLES //////////
string operador;
string codigoTurno;
int capacidad;
bool ticketActivo;
int ticketsCreados;
int ticketsCerrados;
double dineroRecaudado;
int tiempoSimulado;
string placa;
int tipoVehiculo;
string cliente;
int minutoEntrada;
int minutosEstacionados;
double montoFinal;
bool esVIP;
bool salida;
string temp;

////////// DECLARACION COLORES //////////
const ConsoleColor error = ConsoleColor.Red;
const ConsoleColor menu = ConsoleColor.Cyan;
const ConsoleColor confirmar = ConsoleColor.Yellow;

////////// REGISTRO INICIAL //////////

do {
    /// Solicitar nombre del operador
    Console.Write("Ingrese su nombre: ");
    operador = Console.ReadLine() ?? "\n";

    /// Solicitar Codigo de Turno
    codigoTurno = "";
    Console.Write("Ingrese su codigo de turno: ");
    for (int i = 0; i < 4; i++)
    {   // Bucle Permite Solo 4 Digitos
        char tempC = Console.ReadKey().KeyChar;
        // No se permiten espacios
        if (tempC.CompareTo(' ') == 0) { i--; continue; }
        codigoTurno += tempC;
    }
    Console.WriteLine();

    /// Solicitar capacidad del parqueo
    while (true) {
        Console.Write("Ingrese la capacidad del parqueo: ");
        temp = Console.ReadLine() ?? "0\n";
        capacidad = temp.Length == 0 ? 0 : int.Parse(temp);
        if (capacidad >= 10) break;
        Console.ForegroundColor = error;
        Console.WriteLine("Capacidad debe ser mayor o igual a 10");
        Console.ResetColor();
    }

    // Esperar confirmación de usuario
    Console.ForegroundColor = confirmar;
    Console.Write(":: Capacidad ");
    Console.ResetColor();
    Console.Write(capacidad);
    Console.ForegroundColor = confirmar;
    Console.Write(" y turno ");
    Console.ResetColor();
    Console.Write(codigoTurno);
    Console.ForegroundColor = confirmar;
    Console.Write(" [S/n] ");
    Console.ResetColor();
    temp = Console.ReadLine() ?? "";
    Console.Clear();
} while (temp == "n" || temp == "N");

////////// MENU PRINCIPAL (BUCLE) //////////
while (true)
{
    Console.WriteLine("""
     ▄█▀▀▀█▄█              
    ▄██    ▀█           
    ▀███▄     
      ▀█████▄ ▀████████▄        
    ▄     ▀██   ██   ▀██    ▄▀▀ █▄ ▄█ ▄▀▄ █▀▄ ▀█▀   █▀▄ ▄▀▄ █▀▄ █▄▀
    ██     ██   ██    ██    ▄██ █ ▀ █ █▀█ █▀▄  █    █▀  █▀█ █▀▄ █ █
    █▀█████▀    ██   ▄██ 
                ██████▀ 
                ██      
              ▄████▄
    """);
}
////////// DIAGRAMA DE REGISTRO IN-OUT //////////

////////// MOSTRAR ESTADO //////////

////////// SIMULAR TIEMPO //////////

////////// PROGRAMA TERMINA //////////
