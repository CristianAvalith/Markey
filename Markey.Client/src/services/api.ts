const API_BASE_URL = 'http://localhost:5172';

interface ApiResponse<T> {
    code: number;
    data: T;
    messages: string[] | null;
}

export interface LoginRequest {
    userName: string;
    password: string;
}

export interface LoginResponse {
    accessToken: string;
}

export interface RegisterRequest {
    userName: string;
    email: string;
    fullName: string;
    occupationId: string;
    phoneNumber: string;
    password: string;
    photo?: File;
}

export interface User {
    id: string;
    userName: string;
    email: string;
    fullName: string;
    occupation: {
        id: string;
        name: string;
    };
    phoneNumber: string;
    photoUrl: string;
}

export interface UpdateUserRequest {
    fullName: string;
    phoneNumber: string;
    occupationId: string;
}

export interface UserListItem {
    id: string;
    userName: string;
    fullName: string;
    phoneNumber: string;
    email: string;
    photoUrl: string;
    occupation: {
        id: string;
        name: string;
    };
}

export interface UserListResponse {
    users: UserListItem[];
    count: number;
    pageNumber: number;
}

export interface Occupation {
    id: string;
    name: string;
}

class ApiService {
    private getAuthHeaders(includeContentType: boolean = true): Record<string, string> {
        const token = localStorage.getItem('token');
        const userId = localStorage.getItem('userId');
        const headers: Record<string, string> = {};

        if (includeContentType) {
            headers['Content-Type'] = 'application/json';
        }

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }

        if (userId) {
            headers['userId'] = userId;
        }

        return headers;
    }

    async login(data: LoginRequest): Promise<LoginResponse> {
        const response = await fetch(`${API_BASE_URL}/auth/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });

        if (!response.ok) {
            const error = await response.json().catch(() => ({ messages: ['Error al iniciar sesión'] }));
            throw new Error(error.messages?.[0] || 'Error al iniciar sesión');
        }

        const result: ApiResponse<LoginResponse> = await response.json();
        return result.data;
    }

    async register(data: RegisterRequest): Promise<{ userId: string }> {
        const formData = new FormData();
        formData.append('userName', data.userName);
        formData.append('fullName', data.fullName);
        formData.append('email', data.email);
        formData.append('password', data.password);
        formData.append('phoneNumber', data.phoneNumber);
        formData.append('occupationId', data.occupationId);

        if (data.photo) {
            formData.append('photo', data.photo);
        }

        const response = await fetch(`${API_BASE_URL}/auth/register`, {
            method: 'POST',
            body: formData,
        });

        if (!response.ok) {
            const error = await response.json().catch(() => ({ messages: ['Error al registrar usuario'] }));
            throw new Error(error.messages?.[0] || 'Error al registrar usuario');
        }

        const result: ApiResponse<{ userId: string }> = await response.json();
        return result.data;
    }

    async getUser(): Promise<User> {
        const response = await fetch(`${API_BASE_URL}/user`, {
            method: 'GET',
            headers: this.getAuthHeaders(),
        });

        if (!response.ok) {
            const error = await response.json().catch(() => ({ messages: ['Error al obtener usuario'] }));
            throw new Error(error.messages?.[0] || 'Error al obtener usuario');
        }

        const result: ApiResponse<User> = await response.json();
        return result.data;
    }

    async listUsers(fullName?: string, pageSize = 10, pageNumber = 1): Promise<UserListResponse> {
        const params = new URLSearchParams({
            pageSize: pageSize.toString(),
            pageNumber: pageNumber.toString(),
        });

        if (fullName) {
            params.append('fullName', fullName);
        }

        const response = await fetch(`${API_BASE_URL}/user/list?${params}`, {
            method: 'GET',
            headers: this.getAuthHeaders(),
        });

        if (!response.ok) {
            const error = await response.json().catch(() => ({ messages: ['Error al listar usuarios'] }));
            throw new Error(error.messages?.[0] || 'Error al listar usuarios');
        }

        const result: ApiResponse<UserListResponse> = await response.json();
        return result.data;
    }

    async updateUser(data: UpdateUserRequest): Promise<User> {
        const response = await fetch(`${API_BASE_URL}/user`, {
            method: 'PUT',
            headers: this.getAuthHeaders(),
            body: JSON.stringify(data),
        });

        if (!response.ok) {
            const error = await response.json().catch(() => ({ messages: ['Error al actualizar usuario'] }));
            throw new Error(error.messages?.[0] || 'Error al actualizar usuario');
        }

        const result: ApiResponse<User> = await response.json();
        return result.data;
    }

    async updateUserPhoto(photo: File): Promise<{ photoUrl: string }> {
        const formData = new FormData();
        formData.append('photo', photo);

        const response = await fetch(`${API_BASE_URL}/user/photo`, {
            method: 'PUT',
            headers: this.getAuthHeaders(false), // Sin Content-Type para FormData
            body: formData,
        });

        if (!response.ok) {
            const error = await response.json().catch(() => ({ messages: ['Error al actualizar foto'] }));
            throw new Error(error.messages?.[0] || 'Error al actualizar foto');
        }

        const result: ApiResponse<{ photoUrl: string }> = await response.json();
        return result.data;
    }

    async getOccupations(): Promise<Occupation[]> {
        const response = await fetch(`${API_BASE_URL}/catalog/occupations`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            const error = await response.json().catch(() => ({ messages: ['Error al obtener ocupaciones'] }));
            throw new Error(error.messages?.[0] || 'Error al obtener ocupaciones');
        }

        const result = await response.json();
        return result.data.occupations;
    }
}

export const api = new ApiService();
