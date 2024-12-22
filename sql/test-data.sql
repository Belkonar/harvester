-- 1. E-COMMERCE DATABASE
CREATE DATABASE ecommerce;
\c ecommerce;

CREATE TABLE customers (
                           customer_id SERIAL PRIMARY KEY,
                           email VARCHAR(255) UNIQUE NOT NULL,
                           first_name VARCHAR(100),
                           last_name VARCHAR(100),
                           created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE products (
                          product_id SERIAL PRIMARY KEY,
                          name VARCHAR(255) NOT NULL,
                          description TEXT,
                          price DECIMAL(10,2) NOT NULL,
                          stock_quantity INTEGER NOT NULL,
                          category VARCHAR(100)
);

CREATE TABLE orders (
                        order_id SERIAL PRIMARY KEY,
                        customer_id INTEGER REFERENCES customers(customer_id),
                        order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        status VARCHAR(50) DEFAULT 'pending',
                        total_amount DECIMAL(10,2)
);

CREATE TABLE order_items (
                             order_id INTEGER REFERENCES orders(order_id),
                             product_id INTEGER REFERENCES products(product_id),
                             quantity INTEGER NOT NULL,
                             price_at_time DECIMAL(10,2) NOT NULL,
                             PRIMARY KEY (order_id, product_id)
);

-- Insert sample data
INSERT INTO customers (email, first_name, last_name) VALUES
                                                         ('john.doe@email.com', 'John', 'Doe'),
                                                         ('jane.smith@email.com', 'Jane', 'Smith'),
                                                         ('bob.wilson@email.com', 'Bob', 'Wilson');

INSERT INTO products (name, description, price, stock_quantity, category) VALUES
                                                                              ('Laptop Pro', '15-inch laptop with SSD', 1299.99, 50, 'Electronics'),
                                                                              ('Wireless Mouse', 'Ergonomic wireless mouse', 29.99, 200, 'Accessories'),
                                                                              ('Coffee Maker', 'Programmable coffee maker', 79.99, 30, 'Appliances');

INSERT INTO orders (customer_id, status, total_amount) VALUES
                                                           (1, 'completed', 1329.98),
                                                           (2, 'pending', 79.99),
                                                           (3, 'processing', 29.99);

INSERT INTO order_items (order_id, product_id, quantity, price_at_time) VALUES
                                                                            (1, 1, 1, 1299.99),
                                                                            (1, 2, 1, 29.99),
                                                                            (2, 3, 1, 79.99),
                                                                            (3, 2, 1, 29.99);

-- 2. LIBRARY MANAGEMENT SYSTEM
CREATE DATABASE library;
\c library;

CREATE TABLE members (
                         member_id SERIAL PRIMARY KEY,
                         name VARCHAR(200) NOT NULL,
                         email VARCHAR(255) UNIQUE NOT NULL,
                         join_date DATE DEFAULT CURRENT_DATE,
                         member_type VARCHAR(50) DEFAULT 'standard'
);

CREATE TABLE books (
                       book_id SERIAL PRIMARY KEY,
                       title VARCHAR(255) NOT NULL,
                       author VARCHAR(200),
                       isbn VARCHAR(13) UNIQUE,
                       publication_year INTEGER,
                       copies_available INTEGER DEFAULT 0
);

CREATE TABLE loans (
                       loan_id SERIAL PRIMARY KEY,
                       book_id INTEGER REFERENCES books(book_id),
                       member_id INTEGER REFERENCES members(member_id),
                       loan_date DATE DEFAULT CURRENT_DATE,
                       due_date DATE,
                       returned_date DATE
);

-- Insert sample data
INSERT INTO members (name, email, member_type) VALUES
                                                   ('Alice Johnson', 'alice.j@email.com', 'premium'),
                                                   ('Carlos Rodriguez', 'carlos.r@email.com', 'standard'),
                                                   ('Sarah Williams', 'sarah.w@email.com', 'student');

INSERT INTO books (title, author, isbn, publication_year, copies_available) VALUES
                                                                                ('The Great Gatsby', 'F. Scott Fitzgerald', '9780743273565', 1925, 3),
                                                                                ('1984', 'George Orwell', '9780451524935', 1949, 2),
                                                                                ('Pride and Prejudice', 'Jane Austen', '9780141439518', 1813, 4);

INSERT INTO loans (book_id, member_id, loan_date, due_date, returned_date) VALUES
                                                                               (1, 1, '2024-01-01', '2024-01-15', '2024-01-14'),
                                                                               (2, 2, '2024-01-05', '2024-01-19', NULL),
                                                                               (3, 3, '2024-01-10', '2024-01-24', NULL);

-- 3. FITNESS TRACKING APP
CREATE DATABASE fitness;
\c fitness;

CREATE TABLE users (
                       user_id SERIAL PRIMARY KEY,
                       username VARCHAR(50) UNIQUE NOT NULL,
                       email VARCHAR(255) UNIQUE NOT NULL,
                       join_date DATE DEFAULT CURRENT_DATE,
                       weight_kg DECIMAL(5,2),
                       height_cm INTEGER
);

CREATE TABLE workouts (
                          workout_id SERIAL PRIMARY KEY,
                          user_id INTEGER REFERENCES users(user_id),
                          workout_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                          duration_minutes INTEGER,
                          type VARCHAR(50),
                          calories_burned INTEGER
);

CREATE TABLE exercises (
                           exercise_id SERIAL PRIMARY KEY,
                           name VARCHAR(100) NOT NULL,
                           category VARCHAR(50),
                           equipment_needed VARCHAR(100)
);

CREATE TABLE workout_exercises (
                                   workout_id INTEGER REFERENCES workouts(workout_id),
                                   exercise_id INTEGER REFERENCES exercises(exercise_id),
                                   sets INTEGER,
                                   reps INTEGER,
                                   weight_kg DECIMAL(5,2),
                                   PRIMARY KEY (workout_id, exercise_id)
);

-- Insert sample data
INSERT INTO users (username, email, weight_kg, height_cm) VALUES
                                                              ('fitness_fanatic', 'fanatic@email.com', 70.5, 175),
                                                              ('gym_lover', 'lover@email.com', 65.0, 168),
                                                              ('health_nut', 'health@email.com', 80.2, 182);

INSERT INTO exercises (name, category, equipment_needed) VALUES
                                                             ('Bench Press', 'Strength', 'Barbell, Bench'),
                                                             ('Running', 'Cardio', 'None'),
                                                             ('Squats', 'Strength', 'Barbell, Rack');

INSERT INTO workouts (user_id, duration_minutes, type, calories_burned) VALUES
                                                                            (1, 60, 'Strength', 400),
                                                                            (2, 45, 'Cardio', 350),
                                                                            (3, 90, 'Mixed', 600);

INSERT INTO workout_exercises (workout_id, exercise_id, sets, reps, weight_kg) VALUES
                                                                                   (1, 1, 3, 10, 60.0),
                                                                                   (2, 2, 1, 1, NULL),
                                                                                   (3, 3, 4, 8, 80.0);


-- Create the CMS database
CREATE DATABASE cms;
\c cms;

-- Create enum for content status
CREATE TYPE content_status AS ENUM ('draft', 'published', 'archived');

-- Create the content table with JSONB
CREATE TABLE content (
                         content_id SERIAL PRIMARY KEY,
                         title VARCHAR(255) NOT NULL,
                         slug VARCHAR(255) UNIQUE NOT NULL,
                         status content_status DEFAULT 'draft',
                         content_type VARCHAR(50) NOT NULL,
                         metadata JSONB NOT NULL,
                         created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                         updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create an index on the JSONB column for better query performance
CREATE INDEX idx_metadata ON content USING GIN (metadata);

-- Insert sample data with different content types and JSONB structures
INSERT INTO content (title, slug, status, content_type, metadata) VALUES
-- Blog post with author info, tags, and SEO metadata
('Getting Started with JSONB', 'getting-started-jsonb', 'published', 'blog_post', '{
  "author": {
    "id": 1,
    "name": "Jane Smith",
    "email": "jane@example.com"
  },
  "tags": ["postgresql", "json", "database"],
  "word_count": 1200,
  "seo": {
    "meta_description": "Learn how to use JSONB in PostgreSQL",
    "keywords": ["postgresql", "jsonb", "tutorial"],
    "og_image": "https://example.com/images/jsonb-tutorial.jpg"
  },
  "reading_time": 5
}'),

-- Product page with variants and pricing
('Ergonomic Office Chair', 'ergonomic-office-chair', 'published', 'product', '{
  "product_details": {
    "sku": "CHAIR-001",
    "brand": "ErgoMax",
    "category": "Office Furniture",
    "variants": [
      {
        "color": "Black",
        "price": 299.99,
        "stock": 15,
        "dimensions": {
          "width": 60,
          "height": 120,
          "depth": 65
        }
      },
      {
        "color": "Gray",
        "price": 299.99,
        "stock": 8,
        "dimensions": {
          "width": 60,
          "height": 120,
          "depth": 65
        }
      }
    ]
  },
  "shipping": {
    "weight": 15.5,
    "free_shipping": true,
    "assembly_required": true
  }
}'),

-- Event page with location and schedule
('Tech Conference 2024', 'tech-conference-2024', 'draft', 'event', '{
  "event_details": {
    "date": "2024-09-15",
    "location": {
      "venue": "Tech Convention Center",
      "address": "123 Innovation Street",
      "city": "San Francisco",
      "coordinates": {
        "lat": 37.7749,
        "lng": -122.4194
      }
    },
    "schedule": [
      {
        "time": "09:00",
        "title": "Registration",
        "duration": 60
      },
      {
        "time": "10:00",
        "title": "Keynote Speech",
        "speaker": "John Tech",
        "duration": 90
      }
    ]
  },
  "tickets": {
    "early_bird": 299.99,
    "regular": 399.99,
    "vip": 599.99
  },
  "sponsors": [
    {
      "name": "TechCorp",
      "level": "platinum",
      "logo_url": "https://example.com/techcorp.png"
    },
    {
      "name": "DevTools Inc",
      "level": "gold",
      "logo_url": "https://example.com/devtools.png"
    }
  ]
}')