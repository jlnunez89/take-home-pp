import React, { useState, useEffect } from 'react';
import { Customer } from '@/types/api';
import { apiClient } from '@/services/api';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Plus, Phone, User } from 'lucide-react';

interface CustomerListProps {
  onSelectCustomer: (customer: Customer) => void;
  onCreateCustomer: () => void;
  onCustomersChange?: () => void;
}

export const CustomerList: React.FC<CustomerListProps> = ({
  onSelectCustomer,
  onCreateCustomer,
}) => {
  const [customers, setCustomers] = useState<Customer[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    loadCustomers();
  }, []);

  const loadCustomers = async () => {
    try {
      setIsLoading(true);
      const data = await apiClient.getCustomers();
      setCustomers(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to load customers');
    } finally {
      setIsLoading(false);
    }
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString();
  };

  if (isLoading) {
    return (
      <Card>
        <CardHeader>
          <CardTitle>Customers</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="text-center py-8">Loading customers...</div>
        </CardContent>
      </Card>
    );
  }

  if (error) {
    return (
      <Card>
        <CardHeader>
          <CardTitle>Customers</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="text-center py-8 text-destructive">{error}</div>
        </CardContent>
      </Card>
    );
  }

  return (
    <Card>
      <CardHeader>
        <div className="flex items-center justify-between">
          <div>
            <CardTitle>Customers</CardTitle>
            <CardDescription>
              {customers.length} customer{customers.length !== 1 ? 's' : ''}
            </CardDescription>
          </div>
          <Button onClick={onCreateCustomer} size="sm">
            <Plus className="h-4 w-4 mr-2" />
            New Customer
          </Button>
        </div>
      </CardHeader>
      <CardContent>
        {customers.length === 0 ? (
          <div className="text-center py-8 text-muted-foreground">
            No customers yet. Create your first customer to get started.
          </div>
        ) : (
          <div className="space-y-2">
            {customers.map((customer) => (
              <Card
                key={customer.id}
                className="cursor-pointer transition-colors hover:bg-accent"
                onClick={() => onSelectCustomer(customer)}
              >
                <CardContent className="p-4">
                  <div className="flex items-center space-x-3">
                    <User className="h-8 w-8 text-muted-foreground" />
                    <div className="flex-1">
                      <h3 className="font-medium">{customer.name}</h3>
                      <div className="flex items-center text-sm text-muted-foreground">
                        <Phone className="h-3 w-3 mr-1" />
                        {customer.phone}
                      </div>
                      <p className="text-xs text-muted-foreground">
                        Created: {formatDate(customer.createdAt)}
                      </p>
                    </div>
                  </div>
                </CardContent>
              </Card>
            ))}
          </div>
        )}
      </CardContent>
    </Card>
  );
};
