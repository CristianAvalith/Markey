import React, { createContext, useContext, useState, useEffect } from 'react';
import { api, LoginRequest, RegisterRequest, User } from '@/services/api';
import { useNavigate } from 'react-router-dom';
import { toast } from 'sonner';

interface AuthContextType {
    user: User | null;
    token: string | null;
    login: (data: LoginRequest) => Promise<void>;
    register: (data: RegisterRequest) => Promise<void>;
    logout: () => void;
    isLoading: boolean;
    refreshUser: () => Promise<void>;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [user, setUser] = useState<User | null>(null);
    const [token, setToken] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchUser = async () => {
            try {
                const data = await api.getUser();
                setUser(data);
            } catch {
                setUser(null);
            } finally {
                setIsLoading(false);
            }
        };

        if (localStorage.getItem('token')) {
            fetchUser();
        } else {
            setIsLoading(false); 
        }
    }, []);

    const refreshUser = async (currentToken?: string) => {
        try {
            if (!currentToken && !token) return;
            const userData = await api.getUser();
            setUser(userData);
        } catch (error) {
            console.error('Error refreshing user:', error);
            toast.warning('No se pudo obtener los datos del usuario');
        }
    };

    const login = async (data: LoginRequest) => {
        try {
            setIsLoading(true);
            const response = await api.login(data);

            localStorage.setItem('token', response.accessToken);
            setToken(response.accessToken);

            const userData = await api.getUser();
            setUser(userData);

            toast.success('¡Bienvenido!');
            navigate('/dashboard');
        } catch (error) {
            const message = error instanceof Error ? error.message : 'Error al iniciar sesión';
            toast.error(message);
            throw error;
        } finally {
            setIsLoading(false);
        }
    };


    const register = async (data: RegisterRequest) => {
        try {
            setIsLoading(true);
            await api.register(data);
            toast.success('Usuario registrado exitosamente. Por favor inicia sesión.');
            navigate('/login');
        } catch (error) {
            const message = error instanceof Error ? error.message : 'Error al registrar usuario';
            toast.error(message);
            throw error;
        } finally {
            setIsLoading(false);
        }
    };

    const logout = () => {
        localStorage.removeItem('token');
        setToken(null);
        setUser(null);
        navigate('/login');
        toast.info('Sesión cerrada');
    };

    return (
        <AuthContext.Provider value={{ user, token, login, register, logout, isLoading, refreshUser }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
};
