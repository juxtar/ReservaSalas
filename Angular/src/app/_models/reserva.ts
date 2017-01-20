import { Sala } from './sala';
import { Empleado } from './empleado';

export class Reserva {
    ID?: number;
    Inicio: string;
    Fin: string;
    Responsable: Empleado;
    Cantidad: number;
    Sala: Sala;
    Motivo: string;
    Servicio: boolean;
    Almuerzo: boolean;
    Proyector: boolean;
    Anulada: boolean;
}