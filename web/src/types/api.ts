export interface User {
  id: number;
  email: string;
}

export interface Customer {
  id: number;
  name: string;
  phone: string;
  createdAt: string;
  updatedAt: string;
}

export interface Matter {
  id: number;
  title: string;
  description: string;
  status: string;
  customerId: number;
  createdAt: string;
  updatedAt: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface SignupRequest {
  email: string;
  password: string;
  name: string;
  firmName: string;
}

export interface CreateCustomerRequest {
  name: string;
  phone: string;
}

export interface CreateMatterRequest {
  title: string;
  description: string;
  status: string;
}

export interface AuthResponse {
  token: string;
  user: User;
}
