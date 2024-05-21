import { HttpContext, HttpErrorResponse, HttpHeaders, HttpParams } from "@angular/common/http";

export interface Options {
    headers?: HttpHeaders | {
        [header: string]: string | string[];
    };
    context?: HttpContext;
    observe?: 'body';
    params?: HttpParams | {
        [param: string]: string | number | boolean | ReadonlyArray<string | number | boolean>;
    };
    reportProgress?: boolean;
    responseType: 'json';
    withCredentials?: boolean;
}

export interface UserDto {
    id: number,
    username: string,
    mailAddress: string
}

export interface LoginRequest {
    mailAddress: string,
    password: string
}
export interface LoginResponse {
    user: UserDto
}
export interface AuthenticatedResponse{
    error: HttpErrorResponse
    user?: UserDto
}
export interface ProblemDetails {
    type: string,
    title: string,
    status: number,
    instance: string,
    traceId: string,
    errors: {
        name: string,
        reason: string
    }[]
}

export interface RegisterRequest {
    mailAddress: string,
    username: string,
    password: string
}
export interface RegisterResponse {
    message: string
}

export interface GetServicesResponse {
    services: OfferedService[]
}

export interface OfferedService {
    id: number,
    owner: UserDto | null,
    name: string,
    description: string,
    location: LocationDto
    timeSlots?: TimeSlotDto[]
}
export interface TimeSlotStateDto{
    timeSlot: string,
    start: string,
    end: string,
    available: boolean,
    reservedById: number
    blocked: boolean
}
export interface TimeSlotDto{
    start: string,
    end: string,
    fullSpan: string
}

export interface GetTimeSlotsResponse {
    timeSlots: TimeSlotStateDto[]
}

export interface GetServicesByOwnerIdResponse {
    services: OfferedService[]
}

export interface RemoveServiceResponse {
    message: string
}

export interface CreateServiceResponse {
    message: string
}

export interface UpdateServiceResponse {
    message: string
}

export interface ReservationListResponse {
    reservations: ReservationDto[]
}
export interface ReservationDto {
    id: number,
    service?: OfferedService,
    dateStart: string,
    dateEnd: string,
    timeSlot: string
    user?: UserDto
}

export interface LocationDto {
    country: string,
    city: string,
    address: string
}

export interface CountryDto{
    name: string,
    cities: string[]
}

export interface GetCountriesResponse {
    countries: CountryDto[]
}

export interface NotificationDto {
    subject: string,
    content: string,
    timestamp: string,
    isRead: boolean,
    id: number
}

export interface GetMyNotificationsResponse {
    notifications: NotificationDto[]
}