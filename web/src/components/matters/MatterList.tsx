import React, { useState, useEffect } from 'react';
import { Matter, Customer } from '@/types/api';
import { apiClient } from '@/services/api';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Plus, FileText, ArrowLeft } from 'lucide-react';
import { Badge } from '@/components/ui/badge';

interface MatterListProps {
  customer: Customer;
  onCreateMatter: () => void;
  onBack: () => void;
}

export const MatterList: React.FC<MatterListProps> = ({
  customer,
  onCreateMatter,
  onBack,
}) => {
  const [matters, setMatters] = useState<Matter[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    loadMatters();
  }, [customer.id]);

  const loadMatters = async () => {
    try {
      setIsLoading(true);
      const data = await apiClient.getMatters(customer.id);
      setMatters(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to load matters');
    } finally {
      setIsLoading(false);
    }
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString();
  };

  const getStatusColor = (status: string) => {
    switch (status.toLowerCase()) {
      case 'open':
        return 'bg-green-100 text-green-800 border-green-200';
      case 'closed':
        return 'bg-gray-100 text-gray-800 border-gray-200';
      case 'pending':
        return 'bg-yellow-100 text-yellow-800 border-yellow-200';
      default:
        return 'bg-blue-100 text-blue-800 border-blue-200';
    }
  };

  if (isLoading) {
    return (
      <Card>
        <CardHeader>
          <div className="flex items-center space-x-2">
            <Button variant="ghost" size="sm" onClick={onBack}>
              <ArrowLeft className="h-4 w-4" />
            </Button>
            <div>
              <CardTitle>Matters for {customer.name}</CardTitle>
            </div>
          </div>
        </CardHeader>
        <CardContent>
          <div className="text-center py-8">Loading matters...</div>
        </CardContent>
      </Card>
    );
  }

  if (error) {
    return (
      <Card>
        <CardHeader>
          <div className="flex items-center space-x-2">
            <Button variant="ghost" size="sm" onClick={onBack}>
              <ArrowLeft className="h-4 w-4" />
            </Button>
            <div>
              <CardTitle>Matters for {customer.name}</CardTitle>
            </div>
          </div>
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
          <div className="flex items-center space-x-2">
            <Button variant="ghost" size="sm" onClick={onBack}>
              <ArrowLeft className="h-4 w-4" />
            </Button>
            <div>
              <CardTitle>Matters for {customer.name}</CardTitle>
              <CardDescription>
                {matters.length} matter{matters.length !== 1 ? 's' : ''}
              </CardDescription>
            </div>
          </div>
          <Button onClick={onCreateMatter} size="sm">
            <Plus className="h-4 w-4 mr-2" />
            New Matter
          </Button>
        </div>
      </CardHeader>
      <CardContent>
        {matters.length === 0 ? (
          <div className="text-center py-8 text-muted-foreground">
            No matters yet. Create the first matter for this customer.
          </div>
        ) : (
          <div className="space-y-3">
            {matters.map((matter) => (
              <Card key={matter.id} className="border-l-4 border-l-primary">
                <CardContent className="p-4">
                  <div className="flex items-start justify-between">
                    <div className="flex items-start space-x-3 flex-1">
                      <FileText className="h-5 w-5 text-muted-foreground mt-1" />
                      <div className="flex-1">
                        <h3 className="font-medium">{matter.title}</h3>
                        {matter.description && (
                          <p className="text-sm text-muted-foreground mt-1">
                            {matter.description}
                          </p>
                        )}
                        <p className="text-xs text-muted-foreground mt-2">
                          Created: {formatDate(matter.createdAt)}
                        </p>
                        <p className="text-xs text-muted-foreground">
                          Last Updated: {formatDate(matter.updatedAt)}
                        </p>
                      </div>
                    </div>
                    <Badge 
                      variant="outline" 
                      className={getStatusColor(matter.status)}
                    >
                      {matter.status}
                    </Badge>
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
