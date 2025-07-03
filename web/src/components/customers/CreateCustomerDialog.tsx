import React, { useState } from 'react';
import { CreateCustomerRequest } from '@/types/api';
import { apiClient } from '@/services/api';
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';

interface CreateCustomerDialogProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  onCustomerCreated: () => void;
}

export const CreateCustomerDialog: React.FC<CreateCustomerDialogProps> = ({
  open,
  onOpenChange,
  onCustomerCreated,
}) => {
  const [formData, setFormData] = useState<CreateCustomerRequest>({
    name: '',
    phone: '',
  });
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState('');
  const [phoneError, setPhoneError] = useState('');

  const validatePhone = (phone: string): boolean => {
    // Remove all non-digit characters for validation
    const cleanPhone = phone.replace(/\D/g, '');
    
    // Check if it's a valid US phone number (10 digits) or international format
    if (cleanPhone.length === 10) {
      return /^[2-9]\d{2}[2-9]\d{6}$/.test(cleanPhone);
    } else if (cleanPhone.length === 11 && cleanPhone.startsWith('1')) {
      return /^1[2-9]\d{2}[2-9]\d{6}$/.test(cleanPhone);
    }
    
    // Allow international numbers (8-15 digits)
    return cleanPhone.length >= 8 && cleanPhone.length <= 15;
  };

  const formatPhone = (phone: string): string => {
    const cleanPhone = phone.replace(/\D/g, '');
    
    if (cleanPhone.length <= 3) {
      return cleanPhone;
    } else if (cleanPhone.length <= 6) {
      return `(${cleanPhone.slice(0, 3)}) ${cleanPhone.slice(3)}`;
    } else if (cleanPhone.length <= 10) {
      return `(${cleanPhone.slice(0, 3)}) ${cleanPhone.slice(3, 6)}-${cleanPhone.slice(6)}`;
    } else {
      return `+${cleanPhone.slice(0, 1)} (${cleanPhone.slice(1, 4)}) ${cleanPhone.slice(4, 7)}-${cleanPhone.slice(7, 11)}`;
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);
    setError('');
    setPhoneError('');

    // Validate phone number
    if (!validatePhone(formData.phone)) {
      setPhoneError('Please enter a valid phone number');
      setIsLoading(false);
      return;
    }

    try {
      await apiClient.createCustomer(formData);
      setFormData({ name: '', phone: '' });
      setPhoneError('');
      onCustomerCreated();
      onOpenChange(false);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to create customer');
    } finally {
      setIsLoading(false);
    }
  };

  const handleInputChange = (field: keyof CreateCustomerRequest) => (
    e: React.ChangeEvent<HTMLInputElement>
  ) => {
    const value = e.target.value;
    
    if (field === 'phone') {
      // Format phone number as user types
      const formattedPhone = formatPhone(value);
      setFormData(prev => ({ ...prev, [field]: formattedPhone }));
      
      // Clear phone error when user starts typing
      if (phoneError) {
        setPhoneError('');
      }
    } else {
      setFormData(prev => ({ ...prev, [field]: value }));
    }
  };

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="sm:max-w-md">
        <DialogHeader>
          <DialogTitle>Create New Customer</DialogTitle>
          <DialogDescription>
            Add a new customer to your legal practice.
          </DialogDescription>
        </DialogHeader>
        
        <form onSubmit={handleSubmit} className="space-y-4">
          {error && (
            <div className="text-sm text-destructive bg-destructive/10 p-3 rounded-md">
              {error}
            </div>
          )}
          
          <div className="space-y-2">
            <Label htmlFor="name">Customer Name</Label>
            <Input
              id="name"
              value={formData.name}
              onChange={handleInputChange('name')}
              placeholder="Enter customer name"
              required
              disabled={isLoading}
            />
          </div>
          
          <div className="space-y-2">
            <Label htmlFor="phone">Phone Number</Label>
            <Input
              id="phone"
              value={formData.phone}
              onChange={handleInputChange('phone')}
              placeholder="(555) 123-4567"
              required
              disabled={isLoading}
              className={phoneError ? 'border-destructive' : ''}
            />
            {phoneError && (
              <p className="text-sm text-destructive">{phoneError}</p>
            )}
            <p className="text-xs text-muted-foreground">
              Enter a valid US or international phone number
            </p>
          </div>
          
          <DialogFooter>
            <Button
              type="button"
              variant="outline"
              onClick={() => onOpenChange(false)}
              disabled={isLoading}
            >
              Cancel
            </Button>
            <Button type="submit" disabled={isLoading}>
              {isLoading ? 'Creating...' : 'Create Customer'}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
};
