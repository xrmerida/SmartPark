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
                /// Solicitar nombre del operador
                Console.Write("Ingrese su nombre: ");
                operador = Console.ReadLine() ?? "\n";

                /// Solicitar Codigo de Turno
                codigoTurno = "";
                Console.Write("Ingrese su codigo de turno: ");
                for (int i = 0; i < 4; i++)
                {   // Bucle Permite Solo 4 Digitos
                    temp = Console.ReadKey().Key.ToString();
                    // No se permiten espacios
                    if (temp.Length > 2) {
                        // Si es un tecla especial (ej. enter)
                        // no concatenarla
                        i--;
                        continue;
                    } else  if (temp.Length == 2){
                        //*Metodo moderno de substring
                        // elimina el [1] digito
                        // y deja el resto [..] igual
                        //*El indice de un numero
                        // es D[N] por lo que hay que
                        // remove la primer letra
                        temp = temp[1..];
                    }
                    // Concatenar el caracter registrado
                    codigoTurno += temp;
                }
                Console.WriteLine();

                /// Solicitar capacidad del parqueo
                do {
                    Console.Write("Ingrese la capacidad del parqueo: ");
                    temp = Console.ReadLine() ?? "0\n";
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
                temp = Console.ReadLine() ?? "\n";
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
                if (seleccion == "1") {
                    Console.BackgroundColor = menu;
                    Console.ForegroundColor = menuFg;
                    if (ticketActivo) Console.WriteLine("  [1] Registrar salida ");
                    else Console.WriteLine("  [1] Registrar entrada ");
                    Console.ResetColor();
                } else  if (ticketActivo) {
                    Console.WriteLine("  [1] Registrar salida ");
                } else {
                    Console.WriteLine("  [1] Registrar entrada ");
                }

                if (seleccion == "2") {
                    Console.BackgroundColor = menu;
                    Console.ForegroundColor = menuFg;
                    Console.WriteLine("  [2] Simular paso del tiempo ");
                    Console.ResetColor();
                } else {
                    Console.WriteLine("  [2] Simular paso del tiempo ");
                }

                if (seleccion == "3") {
                    Console.BackgroundColor = menu;
                    Console.ForegroundColor = menuFg;
                    Console.WriteLine("  [3] Mostrar estado ");
                    Console.ResetColor();
                } else {
                    Console.WriteLine("  [3] Mostrar estado ");
                }

                if (seleccion == "4") {
                    Console.BackgroundColor = menu;
                    Console.ForegroundColor = menuFg;
                    Console.WriteLine("  [4] Salir del programa ");
                    Console.ResetColor();
                } else {
                    Console.WriteLine("  [4] Salir del programa ");
                }

                // Guardar la tecla presionada por el usuario
                temp = Console.ReadKey().Key.ToString();
                // Tecla enter sera usada para seleccionar
                if (temp != "Enter") {
                    // Se obtiene solamente el segundo valor de
                    // la tecla, esto filtra automaticamente para
                    // solo permitir numeros como entrada
                    temp = temp[1..];
                    // Filtrar todas los indices de teclas
                    // que sean diferentes de 1 (ej. backspace)
                    if (temp.Length == 1) seleccion = temp;
                    // En caso se haya seleccionado la opcion 4
                    // pero no se presionara enter, esto evita que el programa
                    // salga accidentalmente
                    continue;
                }


                ////////// SWITCH DE SUBPROCESOS //////////
                switch (seleccion) {
                    case "1":
                        if (ticketActivo) {
                            // TODO://////// REGISTRO SALIDA //////////
                            ticketActivo = false;
                        } else {
                            // TODO://////// REGISTRO ENTRADA //////////
                            ticketActivo = true;
                        }
                        break;

                    case "2":
                        // TODO://////// SIMULAR TIEMPO //////////
                        break;

                    case "3" or "4":
                        // TODO://////// MOSTRAR ESTADO //////////
                        if (seleccion == "4") {
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
                            // terminara
                            if (temp is not ("n" or "N")) salida = true;
                            Console.ResetColor();
                        }
                        break;
                }
            } while (!salida);
        }
    }
}
