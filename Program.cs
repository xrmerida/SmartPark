namespace SmartPark{
    static class Program
    {
        static void Main()
        {
            ////////// DECLARACION DE VARIABLES //////////
            string operador;
            string codigoTurno;
            int capacidad;
            bool ticketActivo = false;
            int ticketsCreados = 0;
            int ticketsCerrados = 0;
            double dineroRecaudado = 0;
            int tiempoSimulado;
            string placa;
            int tipoVehiculo;
            string cliente;
            int minutoEntrada;
            int minutosEstacionados;
            double montoFinal;
            bool esVIP;
            bool salida = false;
            string temp;

            ////////// DECLARACION COLORES //////////
            const ConsoleColor error = ConsoleColor.Red;
            const ConsoleColor menu = ConsoleColor.Cyan;
            const ConsoleColor confirmar = ConsoleColor.Yellow;

            ////////// REGISTRO INICIAL //////////

            do {
                Console.Clear();
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
                    if (tempC is not (' ' or '\r')) codigoTurno += tempC;
                    else i--;
                }
                Console.WriteLine();

                /// Solicitar capacidad del parqueo
                do {
                    Console.Write("Ingrese la capacidad del parqueo: ");
                    temp = Console.ReadLine() ?? "0\n";
                    capacidad = temp.Length == 0 ? 0 : int.Parse(temp);
                    if (capacidad < 10) {
                        Console.ForegroundColor = error;
                        Console.WriteLine("Capacidad debe ser mayor o igual a 10");
                        Console.ResetColor();
                    }
                } while (capacidad < 10);

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
                temp = Console.ReadLine() ?? "\n";
            } while (temp is "n" or "N");

            ////////// MENU PRINCIPAL (BUCLE) //////////
            Console.WriteLine($"Bienvendo, {operador}");
            while (true)
            {   // Mostrara el menu hasta que el usuario salga
                Console.Clear();
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
                // Mostrar el menu principal
                Console.ForegroundColor = menu;
                if (ticketActivo) {
                    Console.WriteLine("  [1] Registrar salida");
                } else {
                    Console.WriteLine("  [1] Registrar ingreso");
                }
                Console.WriteLine("""
                          [2] Estado del parqueo
                          [3] Simular paso del tiempo
                          [4] Salir
                        """);
                Console.ResetColor();
                Console.Write(":: ");
                temp = Console.ReadLine() ?? "\n";

                // Switch de subprocesos
                switch (temp) {
                    case "1":
                        if (ticketActivo) {
                            ////////// REGISTRO SALIDA //////////
                        } else {
                            ////////// REGISTRO ENTRADA //////////
                        }
                        break;

                    case "2" or "4":
                        ////////// MOSTRAR ESTADO //////////
                        if (salida) {
                            ////////// SALIR DEL PROGRAMA //////////
                        }
                        break;

                    case "3":
                        ////////// SIMULAR TIEMPO //////////
                        break;

                    default:
                        Console.ForegroundColor = error;
                        Console.WriteLine("La opcion no existe");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
