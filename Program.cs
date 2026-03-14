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
            string seleccion = "1";

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
                    temp = Console.ReadKey().Key.ToString();
                    // El indice de las teclas de los numeros
                    // inicia con una D (ej. tecla '1' tiene
                    // indice 'D1')
                    // TrimStart elimina este digito del inicio 
                    // De esta manera, solo se permite si la tecla
                    // registrada es igual a uno (letra o numero)
                    temp = temp.TrimStart('D');
                    if (temp.Length != 1) {
                        // Si es un tecla especial (ej. enter)
                        // no concatenarla
                        i--;
                        continue;
                    }
                    // Concatenar el caracter registrado
                    codigoTurno += temp;
                }
                Console.WriteLine();

                // Solicitar capacidad del parqueo
                do {
                    Console.Write("Ingrese la capacidad del parqueo: ");
                    temp = Console.ReadLine() ?? "0";
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
                // Utilizando al variable seleccion que sera asignada
                // más adelante, se mostrara la opcion seleccionada como
                // en una TUI
                //
                // Seleccion sera el indice que dira que opcion esta seleccionada
                // utilizando clausulas if se cambiara el color dependiendo de 
                // si esta o no seleccionada
                if (seleccion == "D1") {
                    Console.BackgroundColor = menu;
                    Console.ForegroundColor = menuFg;
                    if (ticketActivo) Console.WriteLine(" > [1] Registrar salida ");
                    else Console.WriteLine(" > [1] Registrar entrada ");
                    Console.ResetColor();
                } else  if (ticketActivo) {
                    Console.WriteLine("   [1] Registrar salida ");
                } else {
                    Console.WriteLine("   [1] Registrar entrada ");
                }

                if (seleccion == "D2") {
                    Console.BackgroundColor = menu;
                    Console.ForegroundColor = menuFg;
                    Console.WriteLine(" > [2] Simular paso del tiempo ");
                    Console.ResetColor();
                } else {
                    Console.WriteLine("   [2] Simular paso del tiempo ");
                }

                if (seleccion == "D3") {
                    Console.BackgroundColor = menu;
                    Console.ForegroundColor = menuFg;
                    Console.WriteLine(" > [3] Mostrar estado ");
                    Console.ResetColor();
                } else {
                    Console.WriteLine("   [3] Mostrar estado ");
                }

                if (seleccion == "D4") {
                    Console.BackgroundColor = menu;
                    Console.ForegroundColor = menuFg;
                    Console.WriteLine(" > [4] Salir del programa ");
                    Console.ResetColor();
                } else {
                    Console.WriteLine("   [4] Salir del programa ");
                }

                // Guardar la tecla presionada por el usuario
                temp = Console.ReadKey().Key.ToString();
                if (temp.Length == 2) {
                    // Solo leera numeros (indices 'Dn' donde
                    // n es un numero)
                    seleccion = temp;
                    continue;
                } else if (temp != "Enter") {
                    // Si usuario selecciona la tecla enter
                    // no ejecutuara la sigueinte linea
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
                        if (seleccion == "D4") {
                        Console.Clear();
                        Console.WriteLine("""
                         █▀▄ ██▀ ▄▀▀ █ █ █▄ ▄█ ██▀ █▄ █
                         █▀▄ █▄▄ ▄██ ▀▄█ █ ▀ █ █▄▄ █ ▀█

                        """);
                        }
                        if (seleccion == "D4") {
                            ////////// SALIR DEL PROGRAMA //////////
                            if (ticketActivo)
                            {   // Alerta si hay un ticket activo
                                Console.ForegroundColor = error;
                                Console.WriteLine(" :: Hay un ticket activo!");
                                Console.ResetColor();
                            }
                            Console.ForegroundColor = confirmar;
                            Console.Write(" :: Salir del programa? [S/n] ");
                            temp = Console.ReadLine() ?? "\n";
                            // En caso el usuario responda n o N, el programa
                            // no terminara
                            salida = temp is not ("n" or "N");
                            Console.ResetColor();
                        }
                        break;
                }
            } while (!salida);
        }
    }
}
