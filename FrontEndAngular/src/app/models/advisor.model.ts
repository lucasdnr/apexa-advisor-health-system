export interface Advisor {
    id?: string,
    name: string,
    sinNumber: number | string,
    address?: string,
    phone?: number | string | null,
    healthStatus?: string
}