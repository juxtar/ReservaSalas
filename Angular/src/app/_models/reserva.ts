import { Sala } from './sala';
import { Empleado } from './empleado';
import { Encuesta } from './encuesta';

export class Reserva {
    ID?: number;
    Inicio: string;
    Fin: string;
    Responsable?: Empleado;
    Cantidad: number;
    Sala: Sala;
    Motivo: string;
    Servicio: boolean;
    Almuerzo: boolean;
    Proyector: boolean;
    Anulada?: boolean;
    Encuesta?: Encuesta;
}