import { useRef, useState } from 'react';
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar';
import { Button } from '@/components/ui/button';
import { Camera, Upload } from 'lucide-react';
import { toast } from 'sonner';

interface PhotoUploadProps {
    currentPhotoUrl?: string;
    userName: string;
    onPhotoChange: (file: File) => Promise<void>;
    isLoading?: boolean;
    size?: 'sm' | 'md' | 'lg';
}

const PhotoUpload = ({ 
    currentPhotoUrl, 
    userName, 
    onPhotoChange, 
    isLoading = false,
    size = 'lg'
}: PhotoUploadProps) => {
    const fileInputRef = useRef<HTMLInputElement>(null);
    const [isDragOver, setIsDragOver] = useState(false);

    const sizeClasses = {
        sm: 'h-16 w-16',
        md: 'h-20 w-20',
        lg: 'h-24 w-24'
    };

    const handleFileSelect = (file: File) => {

        if (!file.type.startsWith('image/')) {
            toast.error('Por favor selecciona un archivo de imagen válido');
            return;
        }

        const maxSize = 15 * 1024 * 1024;
        if (file.size > maxSize) {
            toast.error('La imagen no debe superar los 15 MB');
            return;
        }

        const allowedTypes = ['image/jpeg', 'image/jpg', 'image/png'];
        if (!allowedTypes.includes(file.type.toLowerCase())) {
            toast.error('Solo se permiten archivos JPG y PNG');
            return;
        }

        onPhotoChange(file);
    };

    const handleClick = () => {
        if (!isLoading) {
            fileInputRef.current?.click();
        }
    };

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (file) {
            handleFileSelect(file);
        }
    };

    const handleDragOver = (e: React.DragEvent) => {
        e.preventDefault();
        setIsDragOver(true);
    };

    const handleDragLeave = (e: React.DragEvent) => {
        e.preventDefault();
        setIsDragOver(false);
    };

    const handleDrop = (e: React.DragEvent) => {
        e.preventDefault();
        setIsDragOver(false);
        
        const file = e.dataTransfer.files?.[0];
        if (file) {
            handleFileSelect(file);
        }
    };

    const getInitials = () => {
        return userName.split(' ').map(n => n[0]).join('').toUpperCase();
    };

    return (
        <div className="flex flex-col items-center space-y-4">
            <div 
                className={`relative cursor-pointer group ${isLoading ? 'cursor-not-allowed' : ''}`}
                onClick={handleClick}
                onDragOver={handleDragOver}
                onDragLeave={handleDragLeave}
                onDrop={handleDrop}
            >
                <Avatar className={`${sizeClasses[size]} transition-all duration-200 ${isDragOver ? 'scale-105 ring-2 ring-primary' : ''}`}>
                    <AvatarImage 
                        src={currentPhotoUrl} 
                        alt={userName}
                        className="object-cover"
                    />
                    <AvatarFallback className="text-lg font-semibold">
                        {getInitials()}
                    </AvatarFallback>
                </Avatar>

                {/* Overlay con ícono */}
                <div className={`absolute inset-0 bg-black bg-opacity-50 rounded-full flex items-center justify-center transition-opacity duration-200 ${
                    isLoading ? 'opacity-100' : 'opacity-0 group-hover:opacity-100'
                }`}>
                    {isLoading ? (
                        <div className="animate-spin rounded-full h-6 w-6 border-b-2 border-white"></div>
                    ) : (
                        <Camera className="h-6 w-6 text-white" />
                    )}
                </div>
            </div>

            <div className="text-center">
                <Button
                    variant="outline"
                    size="sm"
                    onClick={handleClick}
                    disabled={isLoading}
                    className="text-xs"
                >
                    <Upload className="h-3 w-3 mr-1" />
                    {isLoading ? 'Subiendo...' : 'Cambiar Foto'}
                </Button>
                <p className="text-xs text-muted-foreground mt-1">
                    JPG, PNG hasta 15MB
                </p>
            </div>

            <input
                ref={fileInputRef}
                type="file"
                accept="image/*"
                onChange={handleFileChange}
                className="hidden"
                disabled={isLoading}
            />
        </div>
    );
};

export default PhotoUpload;