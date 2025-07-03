import React, { useState } from 'react';
import { Customer } from '@/types/api';
import { useAuth } from '@/contexts/AuthContext';
import { Button } from '@/components/ui/button';
import { CustomerList } from '@/components/customers/CustomerList';
import { CreateCustomerDialog } from '@/components/customers/CreateCustomerDialog';
import { MatterList } from '@/components/matters/MatterList';
import { CreateMatterDialog } from '@/components/matters/CreateMatterDialog';
import { LogOut, Scale } from 'lucide-react';

export const Dashboard: React.FC = () => {
  const { user, logout } = useAuth();
  const [selectedCustomer, setSelectedCustomer] = useState<Customer | null>(null);
  const [showCreateCustomer, setShowCreateCustomer] = useState(false);
  const [showCreateMatter, setShowCreateMatter] = useState(false);
  const [refreshCustomers, setRefreshCustomers] = useState(0);
  const [refreshMatters, setRefreshMatters] = useState(0);

  const handleCustomerSelect = (customer: Customer) => {
    setSelectedCustomer(customer);
  };

  const handleBackToCustomers = () => {
    setSelectedCustomer(null);
  };

  const handleCustomerCreated = () => {
    setRefreshCustomers(prev => prev + 1);
  };

  const handleMatterCreated = () => {
    setRefreshMatters(prev => prev + 1);
  };

  return (
    <div className="min-h-screen bg-background">
      {/* Header */}
      <header className="border-b">
        <div className="container mx-auto px-4 py-4">
          <div className="flex items-center justify-between">
            <div className="flex items-center space-x-2">
              <Scale className="h-6 w-6" />
              <h1 className="text-xl font-semibold">Legal SaaS</h1>
            </div>
            <div className="flex items-center space-x-4">
              <span className="text-sm text-muted-foreground">
                Welcome, {user?.email}
              </span>
              <Button variant="outline" size="sm" onClick={logout}>
                <LogOut className="h-4 w-4 mr-2" />
                Logout
              </Button>
            </div>
          </div>
        </div>
      </header>

      {/* Main Content */}
      <main className="container mx-auto px-4 py-8">
        <div className="max-w-4xl mx-auto">
          {selectedCustomer ? (
            <MatterList
              key={refreshMatters}
              customer={selectedCustomer}
              onCreateMatter={() => setShowCreateMatter(true)}
              onBack={handleBackToCustomers}
            />
          ) : (
            <CustomerList
              key={refreshCustomers}
              onSelectCustomer={handleCustomerSelect}
              onCreateCustomer={() => setShowCreateCustomer(true)}
            />
          )}
        </div>
      </main>

      {/* Dialogs */}
      <CreateCustomerDialog
        open={showCreateCustomer}
        onOpenChange={setShowCreateCustomer}
        onCustomerCreated={handleCustomerCreated}
      />

      {selectedCustomer && (
        <CreateMatterDialog
          open={showCreateMatter}
          onOpenChange={setShowCreateMatter}
          customer={selectedCustomer}
          onMatterCreated={handleMatterCreated}
        />
      )}
    </div>
  );
};
