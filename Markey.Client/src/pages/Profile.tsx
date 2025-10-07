import { useState, useEffect } from 'react';
import { useAuth } from '@/contexts/AuthContext';
import { api, Occupation } from '@/services/api';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import PhotoUpload from '@/components/PhotoUpload';
import { ArrowLeft, Save } from 'lucide-react';
import { toast } from 'sonner';
import { Link } from 'react-router-dom';

const Profile = () => {
    const { user, refreshUser } = useAuth();
    const [fullName, setFullName] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [occupationId, setOccupationId] = useState('');
    const [occupations, setOccupations] = useState<Occupation[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const [loadingOccupations, setLoadingOccupations] = useState(true);
    const [isUploadingPhoto, setIsUploadingPhoto] = useState(false);

    // Cargar ocupaciones
    useEffect(() => {
        const fetchOccupations = async () => {
            setLoadingOccupations(true);
            try {
                const data = await api.getOccupations();
                setOccupations(data);
            } catch (error) {
                toast.error('Error al cargar ocupaciones');
            } finally {
                setLoadingOccupations(false);
            }
        };

        fetchOccupations();
    }, []);

    // Cuando user y ocupaciones estén listas, setear los campos y occupationId
    useEffect(() => {
        if (user && occupations.length > 0) {
            setFullName(user.fullName);
            setPhoneNumber(user.phoneNumber);

            const currentOccupation = occupations.find(
                occ => occ.id === user.occupation.id
            );
            if (currentOccupation) {
                setOccupationId(currentOccupation.id);
            }
        }
    }, [user, occupations]);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            setIsLoading(true);
            await api.updateUser({
                fullName,
                phoneNumber,
                occupationId,
            });
            await refreshUser();
            toast.success('Perfil actualizado exitosamente');
        } catch (error) {
            const message = error instanceof Error ? error.message : 'Error al actualizar perfil';
            toast.error(message);
        } finally {
            setIsLoading(false);
        }
    };

    const handlePhotoUpload = async (file: File) => {
        try {
            setIsUploadingPhoto(true);
            await api.updateUserPhoto(file);
            await refreshUser();
            toast.success('Foto de perfil actualizada exitosamente');
        } catch (error) {
            const message = error instanceof Error ? error.message : 'Error al actualizar foto';
            toast.error(message);
        } finally {
            setIsUploadingPhoto(false);
        }
    };

    if (!user) return null;

    return (
        <div className="min-h-screen bg-background">
            <header className="border-b bg-card">
                <div className="container mx-auto px-4 py-4">
                    <Link to="/dashboard">
                        <Button variant="ghost" size="sm">
                            <ArrowLeft className="h-4 w-4 mr-2" />
                            Volver al Dashboard
                        </Button>
                    </Link>
                </div>
            </header>

            <main className="container mx-auto px-4 py-8">
                <div className="max-w-2xl mx-auto">
                    <Card>
                        <CardHeader>
                            <div className="flex items-center gap-6">
                                <PhotoUpload
                                    currentPhotoUrl={user.photoUrl}
                                    userName={user.fullName}
                                    onPhotoChange={handlePhotoUpload}
                                    isLoading={isUploadingPhoto}
                                    size="lg"
                                />
                                <div>
                                    <CardTitle className="text-2xl">Mi Perfil</CardTitle>
                                    <CardDescription>
                                        Actualiza tu información personal
                                    </CardDescription>
                                </div>
                            </div>
                        </CardHeader>
                        <CardContent>
                            <form onSubmit={handleSubmit} className="space-y-6">
                                <div className="space-y-2">
                                    <Label htmlFor="fullName">Nombre Completo</Label>
                                    <Input
                                        id="fullName"
                                        type="text"
                                        value={fullName}
                                        onChange={(e) => setFullName(e.target.value)}
                                        required
                                        disabled={isLoading}
                                    />
                                </div>

                                <div className="space-y-2">
                                    <Label htmlFor="phoneNumber">Teléfono</Label>
                                    <Input
                                        id="phoneNumber"
                                        type="tel"
                                        value={phoneNumber}
                                        onChange={(e) => setPhoneNumber(e.target.value)}
                                        required
                                        disabled={isLoading}
                                    />
                                </div>

                                <div className="space-y-2">
                                    <Label htmlFor="occupation">Ocupación</Label>
                                    <Select
                                        value={occupationId}
                                        onValueChange={setOccupationId}
                                        disabled={isLoading || loadingOccupations}
                                    >
                                        <SelectTrigger>
                                            <SelectValue placeholder="Selecciona una ocupación" />
                                        </SelectTrigger>
                                        <SelectContent>
                                            {occupations.map((occupation) => (
                                                <SelectItem key={occupation.id} value={occupation.id}>
                                                    {occupation.name}
                                                </SelectItem>
                                            ))}
                                        </SelectContent>
                                    </Select>
                                </div>

                                <div className="flex gap-4">
                                    <Button type="submit" className="flex-1" disabled={isLoading || !occupationId}>
                                        <Save className="h-4 w-4 mr-2" />
                                        {isLoading ? 'Guardando...' : 'Guardar Cambios'}
                                    </Button>
                                    <Link to="/dashboard" className="flex-1">
                                        <Button type="button" variant="outline" className="w-full">
                                            Cancelar
                                        </Button>
                                    </Link>
                                </div>
                            </form>
                        </CardContent>
                    </Card>
                </div>
            </main>
        </div>
    );
};

export default Profile;
