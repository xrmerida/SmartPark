namespace SmartPark{
    static class Program {
        public static void Main()
        {
            // NOTE://////// DECLARACION DE VARIABLES //////////
            string operador;
            string codigoTurno;
            int capacidad = 0;
            bool ticketActivo = false;
            int ticketsCreados = 0;
            int ticketsCerrados = 0;
            double dineroRecaudado = 0;
            int tiempoSimulado;
            string placa = "";
            int tipoVehiculo = 0;
            string tipoVehiculoStr = "";
            string cliente;
            int minutoEntrada = 0;
            int minutosEstacionados;
            int minutosTranscurridos;
            double montoFinal;
            bool esVIP = false;
            bool salida = false;
            string temp;
            string seleccion = "1";
            bool seleccionExiste = false;

            ////////// DECLARACION COLORES //////////
            const ConsoleColor error = ConsoleColor.Red;
            const ConsoleColor menu = ConsoleColor.Cyan;
            const ConsoleColor menuFg = ConsoleColor.Black;
            const ConsoleColor confirmar = ConsoleColor.Yellow;

            ////////// REGISTRO INICIAL //////////
            do
            {
                Console.Clear();
                // Solicitar nombre del operador
                Console.Write("Ingrese su nombre: ");
                operador = Console.ReadLine() ?? "";

                // Solicitar Codigo de Turno
                codigoTurno = "";
                temp = "";
                Console.ForegroundColor = menu;
                Console.Write("\n :: Presione enter para confirmar");
                Console.ResetColor();
                Console.Write("\eM");
                while (true)
                {
                    // Se necesita un enter para confirmar la seleccion
                    if (codigoTurno.Length > 3) {
                        if (temp == "Enter") break;
                        if (temp == "Backspace") codigoTurno = codigoTurno[..^1];

                    } else {
                        switch (temp.Length)
                        {   // Los caracteres numericos tienen un indice de dos caracteres,
                            // Los caracteres de letras tienen un indice de un caracter
                            // Todas los caracteres especiales tienen un indice mayor a 2
                            case 1:
                                // Concatenar el caracter de letra a codigoTurno
                                codigoTurno += temp;
                                break;
                            case 2:
                                // Se elimina el indice del numero
                                temp = temp[1..];
                                // Se concatena el numero a codigoTurno
                                codigoTurno += temp;
                                break;
                        }

                        switch (temp)
                        {
                            case "Backspace":
                                if (codigoTurno.Length > 0)
                                {   // Cuando se precione Backspace se elimina un caracter
                                    // de codigoTurno, el cual sera impreso despues
                                    codigoTurno = codigoTurno[..^1];
                                }
                                else
                                {   // Si el codigo de turno solo tiene un caracter, se
                                    // elimina todo el contendio de la variable
                                    codigoTurno = "";
                                }
                                break;

                            case "Enter":
                                if (codigoTurno.Length < 4)
                                {   // Cuando se presione Enter, se muestra un error si la 
                                    // cantidad de caracters es menor a 4
                                    Console.ForegroundColor = error;
                                    Console.Write("\n\r\e[2K :: Codigo de turno deben ser 4 digitos!");
                                    Console.ResetColor();
                                    // Regresa el cursor a el ultimo caracter de la linea anterior
                                    Console.Write("\eM");
                                    // Desasignar temp para evitar loop
                                    temp = "";
                                    continue;
                                }
                                break;
                        }
                    }
                    // Se elimina el contendio de la linea con \r\e[J y se imprime de nuevo
                    Console.Write($"\r\e[2KIngrese su codigo de turno: {codigoTurno}");
                    // Se lee el indice del caracter presionado
                    temp = Console.ReadKey(true).Key.ToString() ?? "";
                }
                // Devuelve el cursor a la siguiente linea
                Console.Write("\e[J\n");

                do {
                    Console.Write("\r\e[2KIngrese la capacidad del parqueo: ");
                    temp = Console.ReadLine() ?? "";
                    // Intentar hacer la conversion y devolver
                    // error en caso que no sea posible
                    if (!int.TryParse(temp, out capacidad))
                    {
                        Console.ForegroundColor = error;
                        Console.Write("\r\e[2K :: Ingrese un numero!");
                        Console.ResetColor();
                        Console.Write("\eM");
                        continue;
                    }
                    if (capacidad < 10)
                    {   // Confirmar que la capacidad sea mayor a 10 y continuar 
                        Console.ForegroundColor = error;
                        Console.Write("\r\e[2K :: Capacidad no puede ser meor a 10!");
                        Console.ResetColor();
                        Console.Write("\eM");
                    }
                } while (capacidad < 10);
                Console.Write("\e[J");

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
                // Utilizando al variable seleccion que tiene asignado el valor 1
                // este valor se explica despues cuando se toma la lecutra del 
                // usuario
                //
                // Seleccion sera el indice que dira que opcion esta seleccionada
                // utilizando clausulas if se cambiara el color dependiendo de 
                // si esta o no seleccionada
                // 
                // La siguiente linea es una manera de evitar que la selección no
                // este fuera de rango, si esta fuera regresa a "1"
                seleccionExiste = false;
                if (seleccion == "1") {
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

                if (seleccion == "2") {
                    Console.BackgroundColor = menu;
                    Console.ForegroundColor = menuFg;
                    Console.WriteLine("  > [2] Simular paso del tiempo ");
                    Console.ResetColor();
                    seleccionExiste = true;
                } else {
                    Console.WriteLine("    [2] Simular paso del tiempo ");
                }

                if (seleccion == "3") {
                    Console.BackgroundColor = menu;
                    Console.ForegroundColor = menuFg;
                    Console.WriteLine("  > [3] Mostrar resumen");
                    Console.ResetColor();
                    seleccionExiste = true;
                } else {
                    Console.WriteLine("    [3] Mostrar resumen");
                }

                if (seleccion == "4") {
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
                if (!seleccionExiste) { seleccion = "1"; continue; }

                // Guardar la tecla presionada por el usuario
                temp = Console.ReadKey(true).Key.ToString();
                if (temp.Length == 2) {
                    // Solo leera numeros (indices 'Dn' donde
                    // n es un numero)
                    //
                    // La siguiente linea es un simplificacion del metodo substring
                    // El cual elimina (^) el primer (^1) caracter y deja el resto
                    // de la cadena intacto (..)
                    temp = temp[^1..];
                    seleccion = temp;
                    continue;
                } else if (temp != "Enter") {
                    // El usuario debe presionar la tecla enter para seleccionar
                    // una opcion, de lo contrario se reiniciara el bucle
                    continue;
                }

                ////////// SWITCH DE SUBPROCESOS //////////
                switch (seleccion) {
                    case "1":
                        if (ticketActivo) {
                            // NOTE://////// REGISTRO SALIDA //////////
                            Console.Clear();
                            Console.WriteLine("""
                             ▄▀▀ ▄▀▄ █   █ █▀▄ ▄▀▄
                             ▄██ █▀█ █▄▄ █ █▄▀ █▀█

                            """);
                            Console.ForegroundColor = confirmar;
                            Console.Write("\n :: Desea registrar la salida de ");
                            Console.ResetColor();
                            Console.Write(placa);
                            Console.ForegroundColor = confirmar;
                            Console.Write(" [S/n] ");
                            Console.ResetColor();
                            temp = Console.ReadLine() ?? "";
                            if (temp is "n" or "N") break;

                            // Se obtiene el tiempo que el cliente estuvo estacionado hasta el momento
                            minutosEstacionados = minutoEntrada - (DateTime.Now.Hour * 60);
                            montoFinal = 0;
                            if (minutosEstacionados > 360)
                            {
                                // Se aplica monto fijo en caso este mas de 6h estacionado
                                montoFinal = 25;
                            }
                            else
                            {   // Se calcula el montofinal dependiendo del tipo de vehiculo
                                switch (tipoVehiculo)
                                {
                                    case 1: // Moto
                                        montoFinal += 5 * (minutosEstacionados / 60);
                                        break;
                                    case 2: // Auto
                                        montoFinal += 10 * (minutosEstacionados / 60);
                                        break;
                                    case 3: // Picup/SUV
                                        montoFinal += 15 * (minutosEstacionados / 60);
                                        break;
                                }
                            }

                            if (minutosEstacionados % 60 > 15)
                            {   // Calculo de fraccion de hora
                                montoFinal += minutosEstacionados % 60;
                            }

                            // Descuento para clientes vip del 10%
                            if (esVIP) montoFinal *= 0.90;

                            if (minutosEstacionados > 720)
                            {   // Recargo del 20% por permanencia extrema 
                                montoFinal *= 1.2;
                                Console.ForegroundColor = error;
                                Console.WriteLine(" :: Recargo por permanencia extrema");
                                Console.ResetColor();
                            }

                            dineroRecaudado += montoFinal;
                            ticketsCerrados++;
                            ticketActivo = false;
                            Console.ForegroundColor = confirmar;
                            Console.Write("Monto a pagar: ");
                            Console.ResetColor();
                            Console.WriteLine(montoFinal);
                            Console.ForegroundColor = confirmar;
                            Console.Write("Estadia: ");
                            Console.ResetColor();
                            Console.WriteLine(minutosEstacionados);
                            Console.ForegroundColor = menu;
                            Console.Write(" :: Presione enter para continuar ");
                            Console.ResetColor();
                            Console.ReadLine();
                            break;

                        } else {
                            // NOTE://////// REGISTRO ENTRADA //////////
                            while (true)
                            {   // Mostar la selección de vehiculos como una TUI
                                // Cada opcion sera subrayada si seleccion es igual al
                                // indice de la opcion
                                Console.Clear();
                                Console.WriteLine("""
                                 ██▀ █▄ █ ▀█▀ █▀▄ ▄▀▄ █▀▄ ▄▀▄
                                 █▄▄ █ ▀█  █  █▀▄ █▀█ █▄▀ █▀█

                                """);
                                seleccionExiste = false;
                                if (seleccion == "1") {
                                    Console.BackgroundColor = menu;
                                    Console.ForegroundColor = menuFg;
                                    Console.WriteLine("  > [1] Moto");
                                    Console.ResetColor();
                                    seleccionExiste = true;
                                } else {
                                    Console.WriteLine("    [1] Moto");
                                }

                                if (seleccion == "2") {
                                    Console.BackgroundColor = menu;
                                    Console.ForegroundColor = menuFg;
                                    Console.WriteLine("  > [2] Auto");
                                    Console.ResetColor();
                                    seleccionExiste = true;
                                } else {
                                    Console.WriteLine("    [2] Auto");
                                }

                                if (seleccion == "3") {
                                    Console.BackgroundColor = menu;
                                    Console.ForegroundColor = menuFg;
                                    Console.WriteLine("  > [3] Pickup/SUV");
                                    Console.ResetColor();
                                    seleccionExiste = true;
                                } else {
                                    Console.WriteLine("    [3] Pickup/SUV");
                                }

                                if (seleccion == "4") {
                                    Console.BackgroundColor = menu;
                                    Console.ForegroundColor = menuFg;
                                    Console.WriteLine("  > [4] Canclear");
                                    Console.ResetColor();
                                    seleccionExiste = true;
                                } else {
                                    Console.WriteLine("    [4] Canclear");
                                }
                                Console.ForegroundColor = menu;
                                Console.Write(" :: Presione enter para seleccionar ");
                                Console.ResetColor();
                                // Esta linea evitara que se seleccione una opcion que no existe
                                if (!seleccionExiste) { seleccion = "1"; continue; }

                                // Guardar la tecla presionada por el usuario
                                temp = Console.ReadKey(true).Key.ToString();
                                if (temp.Length == 2) {
                                    // Solo leera numeros (indices 'Dn' donde
                                    // n es un numero)
                                    temp = temp[^1..];
                                    seleccion = temp;
                                    continue;
                                } else if (temp == "Enter") {
                                    // El usuario debe presionar la tecla enter para seleccionar
                                    // una opcion, de lo contrario se reiniciara el bucle
                                    break;
                                }
                            }

                            if (seleccion == "4") break;
                            switch (seleccion)
                            {
                                case "1":
                                    tipoVehiculo = 1;
                                    tipoVehiculoStr = "Moto";
                                    break;
                                case "2":
                                    tipoVehiculo = 2;
                                    tipoVehiculoStr = "Auto";
                                    break;
                                case "3":
                                    tipoVehiculo = 3;
                                    tipoVehiculoStr = "Pickup/SUV";
                                    break;
                            }

                            placa = "";
                            temp = "";
                            Console.ForegroundColor = menu;
                            Console.Write("\n :: Presione enter para confirmar");
                            Console.ResetColor();
                            Console.Write("\eM");
                            while (placa.Length <= 5 || temp != "Enter")
                            {
                                // Se necesita un enter para confirmar la seleccion
                                if (placa.Length > 7) {
                                    if (temp == "Enter") break;
                                    if (temp == "Backspace") placa = placa[..^1];

                                } else {
                                    switch (temp.Length)
                                    {   // Los caracteres numericos tienen un indice de dos caracteres,
                                        // Los caracteres de letras tienen un indice de un caracter
                                        // Todas los caracteres especiales tienen un indice mayor a 2
                                        case 1:
                                            // Concatenar el caracter de letra a placa
                                            placa += temp;
                                            break;
                                        case 2:
                                            // Se elimina el indice del numero
                                            temp = temp[1..];
                                            // Se concatena el numero a placa
                                            placa += temp;
                                            break;
                                    }

                                    switch (temp)
                                    {
                                        case "Backspace":
                                            if (placa.Length > 0)
                                            {   // Cuando se precione Backspace se elimina un caracter
                                                // de placa, el cual sera impreso despues
                                                placa = placa[..^1];
                                            }
                                            else
                                            {   // Si la placa solo tiene un caracter, se
                                                // elimina todo el contendio de la variable
                                                placa = "";
                                            }
                                            break;

                                        case "Enter":
                                            if (placa.Length < 6)
                                            {   // Cuando se presione Enter, se muestra un error si la 
                                                // cantidad de caracters es menor a 6
                                                Console.ForegroundColor = error;
                                                Console.Write("\n\r\e[2K :: Placa deben ser 6-8 digitos!");
                                                Console.ResetColor();
                                                // Regresa el cursor a el ultimo caracter de la linea anterior
                                                Console.Write("\eM");
                                                // Desasignar temp para evitar loop
                                                temp = "";
                                                continue;
                                            }
                                            break;
                                    }
                                }
                                // Se elimina el contendio de la linea con \r\e[J y se imprime de nuevo
                                Console.Write($"\r\e[2KIngrese la placa: {placa}");
                                // Se lee el indice del caracter presionado
                                temp = Console.ReadKey(true).Key.ToString() ?? "";
                            }
                            // Devuelve el cursor a la siguiente linea
                            Console.Write("\e[J\n");

                            Console.Write("Ingrese el nombre del cliente: ");
                            cliente = Console.ReadLine() ?? "";

                            Console.ForegroundColor = confirmar;
                            Console.Write(" :: Es el cliente VIP? [s/N] ");
                            Console.ResetColor();
                            temp = Console.ReadLine() ?? "";
                            esVIP = temp is "s" or "S";

                            Console.ForegroundColor = confirmar;
                            if (esVIP) Console.Write(" :: VIP ");
                            else Console.Write(" :: Cliente ");
                            Console.ResetColor();
                            Console.Write(cliente);
                            Console.ForegroundColor = confirmar;
                            Console.Write(", vehiculo ");
                            Console.ResetColor();
                            Console.Write(tipoVehiculoStr);
                            Console.ForegroundColor = confirmar;
                            Console.Write(" y placa ");
                            Console.ResetColor();
                            Console.Write(placa);
                            Console.ForegroundColor = confirmar;
                            Console.Write("? [S/n] ");
                            Console.ResetColor();
                            temp = Console.ReadLine() ?? "";
                            if (temp is "n" or "N") break;

                            minutoEntrada = DateTime.Now.Minute + (DateTime.Now.Hour * 60);
                            ticketActivo = true;
                            ticketsCreados++;
                            break;
                        }

                    case "2":
                        // NOTE://////// SIMULAR TIEMPO //////////
                        Console.Clear();
                        Console.WriteLine("""
                         ▀█▀ █ ██▀ █▄ ▄█ █▀▄ ▄▀▄
                          █  █ █▄▄ █ ▀ █ █▀  ▀▄▀

                        """);
                        if (!ticketActivo) {
                            Console.ForegroundColor = confirmar;
                            Console.WriteLine(" :: No hay tickets activos");
                            Console.ForegroundColor = menu;
                            Console.Write(" :: Presione enter para regresar ");
                            Console.ResetColor();
                            Console.ReadLine();
                            break;
                        }
                        while (true)
                        {
                            Console.Write("\r\e[0KMinutos a agregar/quitar: ");
                            temp = Console.ReadLine() ?? "0";
                            if (!int.TryParse(temp, out tiempoSimulado))
                            {
                                Console.ForegroundColor = error;
                                Console.Write("\r\e[2K :: Ingrese un numero!");
                                Console.ResetColor();
                                Console.Write("\eM");
                            } else {
                                break;
                            }
                        }

                        Console.ForegroundColor = confirmar;
                        Console.Write(" :: Desea agregar ");
                        Console.ResetColor();
                        Console.Write(tiempoSimulado);
                        Console.ForegroundColor = confirmar;
                        Console.Write("min? [S/n] ");
                        temp = Console.ReadLine() ?? "";
                        if (temp is "n" or "N") break;

                        minutoEntrada += tiempoSimulado;
                        tiempoSimulado =  minutoEntrada - DateTime.Now.Minute -
                            (DateTime.Now.Hour * 60);
                        switch (tiempoSimulado)
                        {
                            case >720:
                                Console.ForegroundColor = error;
                                Console.WriteLine(" :: Recargo por permanencia extrema");
                                break;
                            case >360:
                                Console.ForegroundColor = confirmar;
                                Console.WriteLine(" :: Advertencia de permanencia extrema");
                                break;
                        }
                        Console.ResetColor();

                        Console.ForegroundColor = confirmar;
                        Console.Write("Minutos actuales: ");
                        Console.ResetColor();
                        Console.WriteLine(tiempoSimulado);
                        Console.ForegroundColor = menu;
                        Console.Write(" :: Presione enter para regresar ");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;

                    case "3" or "4":
                        // NOTE://////// MOSTRAR ESTADO //////////
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

                        Console.ForegroundColor = menu;
                        Console.Write("Capacidad: ");
                        Console.ResetColor();
                        Console.WriteLine(capacidad);
                        Console.ForegroundColor = menu;
                        Console.Write("Espacios ocupados: ");
                        Console.ResetColor();
                        Console.WriteLine(ticketsCreados - ticketsCreados);
                        if (ticketActivo)
                        {
                            Console.ForegroundColor = menu;
                            Console.Write("Tiempo transcurrido: ");
                            Console.ResetColor();
                            minutosTranscurridos = minutoEntrada - DateTime.Now.Minute;
                            minutosTranscurridos -= DateTime.Now.Hour * 60;
                            Console.WriteLine(minutosTranscurridos);
                        }
                        Console.ForegroundColor = menu;
                        Console.Write("Total recaudado: ");
                        Console.ResetColor();
                        Console.WriteLine(dineroRecaudado);
                        Console.ForegroundColor = menu;
                        Console.Write("Tickets Creados: ");
                        Console.ResetColor();
                        Console.WriteLine(ticketsCreados);
                        Console.ForegroundColor = menu;
                        Console.Write("Tickets Cerrados: ");
                        Console.ResetColor();
                        Console.WriteLine(ticketsCerrados);

                        if (seleccion == "4")
                        {   // NOTE://////// SALIR DEL PROGRAMA //////////
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
