import { useState, useEffect } from 'react';
import { useAuth } from '@/contexts/AuthContext';
import { api, UserListItem, Occupation } from '@/services/api';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar';
import { LogOut, Search, User as UserIcon } from 'lucide-react';
import { toast } from 'sonner';
import { Link } from 'react-router-dom';

const Dashboard = () => {
    const { user, logout } = useAuth();

    const [users, setUsers] = useState<UserListItem[]>([]);
    const [searchQuery, setSearchQuery] = useState('');
    const [isLoading, setIsLoading] = useState(true);
    const [totalCount, setTotalCount] = useState(0);
    const [pageNumber, setPageNumber] = useState(1);
    const pageSize = 10;

    // ---- Fetch de usuarios ----
    useEffect(() => {
        const fetchUsers = async () => {
            try {
                setIsLoading(true);
                const response = await api.listUsers(searchQuery || undefined, pageSize, pageNumber);
                setUsers(response.users);
                setTotalCount(response.count);
            } catch (error) {
                toast.error('Error al cargar usuarios');
            } finally {
                setIsLoading(false);
            }
        };

        fetchUsers();
    }, [pageNumber, searchQuery]);

    const handleSearch = (e: React.FormEvent) => {
        e.preventDefault();
        setPageNumber(1); 
    };

    const totalPages = Math.ceil(totalCount / pageSize);

    return (
        <div className="min-h-screen bg-background">
            <header className="border-b bg-card">
                <div className="container mx-auto px-4 py-4 flex items-center justify-between">
                    <h1 className="text-2xl font-bold text-foreground">Dashboard</h1>

                    <div className="flex items-center gap-4">
                        <div className="flex items-center gap-2">
                            <Avatar className="h-8 w-8">
                                <AvatarImage src={user?.photoUrl} alt={user?.fullName} />
                                <AvatarFallback>
                                    {user?.fullName.split(' ').map(n => n[0]).join('')}
                                </AvatarFallback>
                            </Avatar>
                            <span className="text-sm font-medium hidden sm:inline">{user?.fullName}</span>
                        </div>

                        <Link to="/profile">
                            <Button variant="outline" size="sm">
                                <UserIcon className="h-4 w-4 mr-2" />
                                Perfil
                            </Button>
                        </Link>

                        <Button variant="outline" size="sm" onClick={logout}>
                            <LogOut className="h-4 w-4 mr-2" />
                            Cerrar Sesión
                        </Button>
                    </div>
                </div>
            </header>

            <main className="container mx-auto px-4 py-8">
                {/* Info usuario */}
                <Card className="mb-6">
                    <CardHeader>
                        <CardTitle>Bienvenido, {user?.fullName}</CardTitle>
                    </CardHeader>
                    <CardContent>
                        <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                            <div className="bg-muted p-4 rounded-lg">
                                <p className="text-sm text-muted-foreground">Email</p>
                                <p className="font-medium">{user?.email}</p>
                            </div>
                            <div className="bg-muted p-4 rounded-lg">
                                <p className="text-sm text-muted-foreground">Teléfono</p>
                                <p className="font-medium">{user?.phoneNumber}</p>
                            </div>
                            <div className="bg-muted p-4 rounded-lg">
                                <p className="text-sm text-muted-foreground">Ocupación</p>
                                <p className="font-medium">{user?.occupation.name}</p>
                            </div>
                        </div>
                    </CardContent>
                </Card>

                {/* Lista de usuarios */}
                <Card>
                    <CardHeader>
                        <div className="flex items-center justify-between">
                            <CardTitle>Lista de Usuarios</CardTitle>

                            <form onSubmit={handleSearch} className="flex gap-2">
                                <Input
                                    type="text"
                                    placeholder="Buscar por nombre..."
                                    value={searchQuery}
                                    onChange={(e) => setSearchQuery(e.target.value)}
                                    className="w-64"
                                />
                                <Button type="submit" size="sm">
                                    <Search className="h-4 w-4" />
                                </Button>
                            </form>
                        </div>
                    </CardHeader>

                    <CardContent>
                        {isLoading ? (
                            <div className="text-center py-8">
                                <div className="h-8 w-8 animate-spin rounded-full border-4 border-primary border-t-transparent mx-auto"></div>
                            </div>
                        ) : users.length === 0 ? (
                            <div className="text-center py-8 text-muted-foreground">
                                No se encontraron usuarios
                            </div>
                        ) : (
                            <div className="space-y-4">
                                {users.map((usr) => (
                                    <div
                                        key={usr.id}
                                        className="flex items-center gap-4 p-4 rounded-lg border bg-card hover:bg-accent/50 transition-colors"
                                    >
                                        <Avatar className="h-12 w-12">
                                            <AvatarFallback>
                                                <img src={usr.photoUrl} alt={usr.fullName} className="object-cover" />
                                            </AvatarFallback>
                                        </Avatar>

                                        <div className="flex-1">
                                            <h3 className="font-semibold">{usr.fullName}</h3>
                                            <p className="text-sm text-muted-foreground">@{usr.userName}</p>
                                            <p className="text-sm text-muted-foreground">{usr.email}</p>
                                            <p className="text-sm text-muted-foreground">{usr.phoneNumber}</p>
                                        </div>

                                        <div className="text-right">
                                            <p className="text-sm font-medium">{usr.occupation.name}</p>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        )}

                        {/* Paginación */}
                        {totalPages > 1 && (
                            <div className="flex items-center justify-center gap-2 mt-6">
                                <Button
                                    variant="outline"
                                    size="sm"
                                    onClick={() => setPageNumber(prev => Math.max(1, prev - 1))}
                                    disabled={pageNumber === 1}
                                >
                                    Anterior
                                </Button>
                                <span className="text-sm text-muted-foreground">
                                    Página {pageNumber} de {totalPages}
                                </span>
                                <Button
                                    variant="outline"
                                    size="sm"
                                    onClick={() => setPageNumber(prev => Math.min(totalPages, prev + 1))}
                                    disabled={pageNumber === totalPages}
                                >
                                    Siguiente
                                </Button>
                            </div>
                        )}
                    </CardContent>
                </Card>
            </main>
        </div>
    );
};

export default Dashboard;
