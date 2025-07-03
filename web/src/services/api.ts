import {
  User,
  Customer,
  Matter,
  LoginRequest,
  SignupRequest,
  CreateCustomerRequest,
  CreateMatterRequest,
  AuthResponse,
} from "@/types/api";

const API_BASE_URL = "http://localhost:8080/api";

class ApiClient {
  private token: string | null = null;

  constructor() {
    this.token = localStorage.getItem("token");
  }

  private async request<T>(
    endpoint: string,
    options: RequestInit = {}
  ): Promise<T> {
    const url = `${API_BASE_URL}${endpoint}`;
    const config: RequestInit = {
      headers: {
        "Content-Type": "application/json",
        ...(this.token && { Authorization: `Bearer ${this.token}` }),
      },
      ...options,
    };

    const response = await fetch(url, config);

    if (!response.ok) {
      const error = await response
        .json()
        .catch(() => ({ message: "An error occurred" }));
      throw new Error(error.message || "Request failed");
    }

    return response.json();
  }

  setToken(token: string) {
    this.token = token;
    localStorage.setItem("token", token);
  }

  clearToken() {
    this.token = null;
    localStorage.removeItem("token");
  }

  // Auth endpoints
  async login(credentials: LoginRequest): Promise<AuthResponse> {
    const response = await this.request<AuthResponse>("/auth/login", {
      method: "POST",
      body: JSON.stringify(credentials),
    });
    this.setToken(response.token);
    return response;
  }

  async signup(userData: SignupRequest): Promise<AuthResponse> {
    const response = await this.request<AuthResponse>("/auth/signup", {
      method: "POST",
      body: JSON.stringify(userData),
    });
    this.setToken(response.token);
    return response;
  }

  async getCurrentUser(): Promise<User> {
    return this.request<User>("/auth/me");
  }

  // Customer endpoints
  async getCustomers(): Promise<Customer[]> {
    return this.request<Customer[]>("/customers");
  }

  async createCustomer(customerData: CreateCustomerRequest): Promise<Customer> {
    return this.request<Customer>("/customers", {
      method: "POST",
      body: JSON.stringify(customerData),
    });
  }

  async getCustomer(customerId: number): Promise<Customer> {
    return this.request<Customer>(`/customers/${customerId}`);
  }

  // Matter endpoints
  async getMatters(customerId: number): Promise<Matter[]> {
    return this.request<Matter[]>(`/customers/${customerId}/matters`);
  }

  async createMatter(
    customerId: number,
    matterData: CreateMatterRequest
  ): Promise<Matter> {
    return this.request<Matter>(`/customers/${customerId}/matters`, {
      method: "POST",
      body: JSON.stringify(matterData),
    });
  }

  async getMatter(customerId: number, matterId: number): Promise<Matter> {
    return this.request<Matter>(`/customers/${customerId}/matters/${matterId}`);
  }
}

export const apiClient = new ApiClient();
