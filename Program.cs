namespace SmartPark{
    static class Program {
        public static void Main()
        {
            ////////// DECLARACION DE VARIABLES //////////
            string operador;
            string codigoTurno;
            int capacidad = 0;
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
            string seleccion = "D1";
            bool seleccionExiste = false;

            ////////// DECLARACION COLORES //////////
            const ConsoleColor error = ConsoleColor.Red;
            const ConsoleColor menu = ConsoleColor.Cyan;
            const ConsoleColor menuFg = ConsoleColor.Black;
            const ConsoleColor confirmar = ConsoleColor.Yellow;

            //////////////////////////////////////
            ////////// REGISTRO INICIAL //////////
            do {
                Console.Clear();
                // Solicitar nombre del operador
                Console.Write("Ingrese su nombre: ");
                operador = Console.ReadLine() ?? "";

                // Solicitar Codigo de Turno
                codigoTurno = "";
                Console.Write("Ingrese su codigo de turno: ");
                for (int i = 0; i < 4; i++)
                {   // Bucle Permite Solo 4 Digitos
                    // Colocar (true) a ReadKey desabilita el echo de caracteres
                    // esto evita que se muestren espacios y enters
                    temp = Console.ReadKey(true).Key.ToString();
                    // Si la tecla registrada tiene más de 2 digitos
                    // implica que es una tecla especial (ej. enter)
                    if (temp.Length > 2) {
                        i--;
                        continue;
                    } else if (temp.Length == 2) {
                        // El indice de las teclas de los numeros inicia con una D
                        // (ej. tecla '1' tiene indice 'D1') y ya que solo los numeros
                        // tiene este indice, son las unicas teclas que tiene 2 caracters
                        //
                        // la siguiente linea es una simplificación del metodo
                        // de .Substring(1) el cual eliminara solamente el primer
                        // caracter
                        temp = temp[1..];
                    }
                    // Ya que ReadKey tiene el echo desactivado,
                    // se imprimen los caracteres permitidos solamente
                    Console.Write(temp);
                    // Concatenar el caracter registrado
                    codigoTurno += temp;
                }
                Console.WriteLine();

                // Solicitar capacidad del parqueo
                do {
                    Console.Write("Ingrese la capacidad del parqueo: ");
                    temp = Console.ReadLine() ?? "";
                    // Intentar hacer la conversion y devolver
                    // error en caso que no sea posible
                    try {
                        capacidad = int.Parse(temp);
                    } catch {
                        Console.ForegroundColor = error;
                        Console.WriteLine(" :: Ingrese un numero!");
                        Console.ResetColor();
                        continue;
                    }
                    if (capacidad < 10)
                    {   // Confirmar que la capacidad sea mayor a 10 y continuar 
                        Console.ForegroundColor = error;
                        Console.WriteLine(" :: Capacidad debe ser mayor o igual a 10!");
                        Console.ResetColor();
                    }
                } while (capacidad < 10);

                // Codificación de salida con colores
                Console.ForegroundColor = confirmar;
                Console.Write(" :: Operador ");
                Console.ResetColor();
                Console.Write(operador);
                Console.ForegroundColor = confirmar;
                Console.Write(", capacidad ");
                Console.ResetColor();
                Console.Write(capacidad);
                Console.ForegroundColor = confirmar;
                Console.Write(" y turno ");
                Console.ResetColor();
                Console.Write(codigoTurno);
                Console.ForegroundColor = confirmar;
                Console.Write("? [S/n] ");
                Console.ResetColor();
                temp = Console.ReadLine() ?? "";
            } while (temp is "n" or "N");

            ////////// MENU PRINCIPAL (BUCLE) //////////
            do
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

                // Mostrar el menu principal como una TUI
                // Utilizando al variable seleccion que tiene asignado el valor D1
                // este valor se explica despues cuando se toma la lecutra del 
                // usuario
                //
                // Seleccion sera el indice que dira que opcion esta seleccionada
                // utilizando clausulas if se cambiara el color dependiendo de 
                // si esta o no seleccionada
                // 
                // La siguiente linea es una manera de evitar que la selección no
                // este fuera de rango, si esta fuera regresa a "D1"
                seleccionExiste = false;
                if (seleccion == "D1") {
                    Console.BackgroundColor = menu;
                    Console.ForegroundColor = menuFg;
                    if (ticketActivo) Console.WriteLine("  > [1] Registrar salida ");
                    else Console.WriteLine("  > [1] Registrar entrada ");
                    Console.ResetColor();
                    seleccionExiste = true;
                } else  if (ticketActivo) {
                    Console.WriteLine("    [1] Registrar salida ");
                } else {
                    Console.WriteLine("    [1] Registrar entrada ");
                }

                if (seleccion == "D2") {
                    Console.BackgroundColor = menu;
                    Console.ForegroundColor = menuFg;
                    Console.WriteLine("  > [2] Simular paso del tiempo ");
                    Console.ResetColor();
                    seleccionExiste = true;
                } else {
                    Console.WriteLine("    [2] Simular paso del tiempo ");
                }

                if (seleccion == "D3") {
                    Console.BackgroundColor = menu;
                    Console.ForegroundColor = menuFg;
                    Console.WriteLine("  > [3] Mostrar estado ");
                    Console.ResetColor();
                    seleccionExiste = true;
                } else {
                    Console.WriteLine("    [3] Mostrar estado ");
                }

                if (seleccion == "D4") {
                    Console.BackgroundColor = menu;
                    Console.ForegroundColor = menuFg;
                    Console.WriteLine("  > [4] Salir del programa ");
                    Console.ResetColor();
                    seleccionExiste = true;
                } else {
                    Console.WriteLine("    [4] Salir del programa ");
                }
                Console.ForegroundColor = menu;
                Console.Write(" :: Presione enter para seleccionar ");
                Console.ResetColor();
                // Esta linea evitara que se seleccione una opcion que no existe
                if (!seleccionExiste) { seleccion = "D1"; continue; }

                // Guardar la tecla presionada por el usuario
                temp = Console.ReadKey(true).Key.ToString();
                if (temp.Length == 2) {
                    // Solo leera numeros (indices 'Dn' donde
                    // n es un numero)
                    seleccion = temp;
                    continue;
                } else if (temp != "Enter") {
                    // El usuario debe presionar la tecla enter para seleccionar
                    // una opcion, de lo contrario se reiniciara el bucle
                    continue;
                }

                ////////// SWITCH DE SUBPROCESOS //////////
                switch (seleccion) {
                    case "D1":
                        if (ticketActivo) {
                            // TODO://////// REGISTRO SALIDA //////////
                            Console.Clear();
                            Console.WriteLine("""
                             ▄▀▀ ▄▀▄ █   █ █▀▄ ▄▀▄
                             ▄██ █▀█ █▄▄ █ █▄▀ █▀█

                            """);
                            ticketActivo = false;
                        } else {
                            // TODO://////// REGISTRO ENTRADA //////////
                            Console.Clear();
                            Console.WriteLine("""
                             ██▀ █▄ █ ▀█▀ █▀▄ ▄▀▄ █▀▄ ▄▀▄
                             █▄▄ █ ▀█  █  █▀▄ █▀█ █▄▀ █▀█

                            """);
                            ticketActivo = true;
                        }
                        break;

                    case "D2":
                        // TODO://////// SIMULAR TIEMPO //////////
                        Console.Clear();
                        Console.WriteLine("""
                         ▀█▀ █ ██▀ █▄ ▄█ █▀▄ ▄▀▄
                          █  █ █▄▄ █ ▀ █ █▀  ▀▄▀

                        """);
                        Console.ReadLine();
                        break;

                    case "D3" or "D4":
                        // TODO://////// MOSTRAR ESTADO //////////
                        Console.Clear();
                        Console.WriteLine("""
                         █▀▄ ██▀ ▄▀▀ █ █ █▄ ▄█ ██▀ █▄ █
                         █▀▄ █▄▄ ▄██ ▀▄█ █ ▀ █ █▄▄ █ ▀█

                        """);
                        if (ticketActivo)
                        {   // Alerta si hay un ticket activo
                            Console.ForegroundColor = confirmar;
                            Console.WriteLine("Hay un ticket activo!");
                            Console.ResetColor();
                        }

                        if (seleccion == "D4")
                        {   ////////// SALIR DEL PROGRAMA //////////
                            Console.ForegroundColor = confirmar;
                            Console.Write(" :: Salir del programa? [S/n] ");
                            temp = Console.ReadLine() ?? "";
                            // Si el usuario seleccion 'n' o 'N'
                            // la variable salida sera asignada
                            // verdadera y el ciclo terminara
                            salida = temp is not ("n" or "N");
                        } else {
                            // Mostrar confirmación para regresar a menu principal
                            Console.ForegroundColor = confirmar;
                            Console.Write(" :: Presione enter para regresar ");
                            Console.ReadLine();
                        }
                        Console.ResetColor();

                        break;
                }
            } while (!salida);
        }
    }
}
