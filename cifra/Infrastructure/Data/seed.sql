-- =============================================
-- Seed Data - Controle de Gastos
-- Tabelas de referência (TiposDespesa, CategoriasDespesa, SubCategoriasDespesa)
-- já são populadas pela migration do EF Core.
-- Este script popula apenas as tabelas transacionais.
-- =============================================

-- =============================================
-- ANOS
-- =============================================
INSERT INTO Anos (Numero) VALUES (2025);
INSERT INTO Anos (Numero) VALUES (2026);

-- =============================================
-- MESES (2025: Jan-Jun | 2026: Jan-Fev)
-- =============================================
-- 2025 (AnoId = 1)
INSERT INTO Meses (AnoId, Nome, Numero) VALUES (1, 'Janeiro', 1);
INSERT INTO Meses (AnoId, Nome, Numero) VALUES (1, 'Fevereiro', 2);
INSERT INTO Meses (AnoId, Nome, Numero) VALUES (1, 'Março', 3);
INSERT INTO Meses (AnoId, Nome, Numero) VALUES (1, 'Abril', 4);
INSERT INTO Meses (AnoId, Nome, Numero) VALUES (1, 'Maio', 5);
INSERT INTO Meses (AnoId, Nome, Numero) VALUES (1, 'Junho', 6);

-- 2026 (AnoId = 2)
INSERT INTO Meses (AnoId, Nome, Numero) VALUES (2, 'Janeiro', 1);
INSERT INTO Meses (AnoId, Nome, Numero) VALUES (2, 'Fevereiro', 2);

-- =============================================
-- RECEITAS
-- =============================================
-- Janeiro/2025 (MesId = 1)
INSERT INTO Receitas (MesId, Descricao, Valor) VALUES (1, 'Salário', 5500.00);
INSERT INTO Receitas (MesId, Descricao, Valor) VALUES (1, 'Freelance', 800.00);

-- Fevereiro/2025 (MesId = 2)
INSERT INTO Receitas (MesId, Descricao, Valor) VALUES (2, 'Salário', 5500.00);

-- Março/2025 (MesId = 3)
INSERT INTO Receitas (MesId, Descricao, Valor) VALUES (3, 'Salário', 5500.00);
INSERT INTO Receitas (MesId, Descricao, Valor) VALUES (3, 'Freelance', 1200.00);

-- Abril/2025 (MesId = 4)
INSERT INTO Receitas (MesId, Descricao, Valor) VALUES (4, 'Salário', 5500.00);

-- Maio/2025 (MesId = 5)
INSERT INTO Receitas (MesId, Descricao, Valor) VALUES (5, 'Salário', 5500.00);
INSERT INTO Receitas (MesId, Descricao, Valor) VALUES (5, 'Venda de eletrônicos', 450.00);

-- Junho/2025 (MesId = 6)
INSERT INTO Receitas (MesId, Descricao, Valor) VALUES (6, 'Salário', 5500.00);

-- Janeiro/2026 (MesId = 7)
INSERT INTO Receitas (MesId, Descricao, Valor) VALUES (7, 'Salário', 6000.00);
INSERT INTO Receitas (MesId, Descricao, Valor) VALUES (7, 'Freelance', 900.00);

-- Fevereiro/2026 (MesId = 8)
INSERT INTO Receitas (MesId, Descricao, Valor) VALUES (8, 'Salário', 6000.00);

-- =============================================
-- INVESTIMENTOS
-- =============================================
-- Janeiro/2025
INSERT INTO Investimentos (MesId, Descricao, Valor) VALUES (1, 'Tesouro Selic', 500.00);
INSERT INTO Investimentos (MesId, Descricao, Valor) VALUES (1, 'CDB Liquidez Diária', 300.00);

-- Fevereiro/2025
INSERT INTO Investimentos (MesId, Descricao, Valor) VALUES (2, 'Tesouro Selic', 500.00);

-- Março/2025
INSERT INTO Investimentos (MesId, Descricao, Valor) VALUES (3, 'Tesouro Selic', 500.00);
INSERT INTO Investimentos (MesId, Descricao, Valor) VALUES (3, 'Fundo Imobiliário', 400.00);

-- Abril/2025
INSERT INTO Investimentos (MesId, Descricao, Valor) VALUES (4, 'Tesouro Selic', 500.00);

-- Maio/2025
INSERT INTO Investimentos (MesId, Descricao, Valor) VALUES (5, 'Tesouro Selic', 500.00);
INSERT INTO Investimentos (MesId, Descricao, Valor) VALUES (5, 'CDB Liquidez Diária', 200.00);

-- Junho/2025
INSERT INTO Investimentos (MesId, Descricao, Valor) VALUES (6, 'Tesouro Selic', 500.00);

-- Janeiro/2026
INSERT INTO Investimentos (MesId, Descricao, Valor) VALUES (7, 'Tesouro Selic', 600.00);
INSERT INTO Investimentos (MesId, Descricao, Valor) VALUES (7, 'Fundo Imobiliário', 400.00);

-- Fevereiro/2026
INSERT INTO Investimentos (MesId, Descricao, Valor) VALUES (8, 'Tesouro Selic', 600.00);

-- =============================================
-- DESPESAS
-- TipoDespesaId: 1=Fixa, 2=Variável, 3=Extra, 4=Adicional
-- =============================================

-- -----------------------------------------------
-- Janeiro/2025 (MesId = 1)
-- -----------------------------------------------
-- Fixas
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 1, 1, 'Aluguel janeiro', 1500.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 1, 2, 'Condomínio janeiro', 350.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 1, 10, 'Internet fibra', 120.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 1, 6, 'Plano celular', 55.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 1, 5, 'Spotify família', 34.90);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 1, 20, 'Plano de saúde', 280.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 1, 11, 'Prestação da moto', 650.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 1, 12, 'Seguro da moto', 89.90);

-- Variáveis
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 2, 3, 'Conta de luz', 185.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 2, 4, 'Conta de água', 95.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 2, 7, 'Gás de cozinha', 110.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 2, 16, 'Supermercado semanal', 620.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 2, 14, 'Combustível', 250.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 2, 19, 'Padaria', 85.00);

-- Extras
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (1, 3, 25, 'Jantar aniversário', 180.00);

-- -----------------------------------------------
-- Fevereiro/2025 (MesId = 2)
-- -----------------------------------------------
-- Fixas
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 1, 1, 'Aluguel fevereiro', 1500.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 1, 2, 'Condomínio fevereiro', 350.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 1, 10, 'Internet fibra', 120.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 1, 6, 'Plano celular', 55.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 1, 5, 'Spotify família', 34.90);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 1, 20, 'Plano de saúde', 280.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 1, 11, 'Prestação da moto', 650.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 1, 12, 'Seguro da moto', 89.90);

-- Variáveis
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 2, 3, 'Conta de luz', 210.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 2, 4, 'Conta de água', 88.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 2, 16, 'Supermercado semanal', 580.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 2, 14, 'Combustível', 230.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 2, 17, 'Feira', 75.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 2, 21, 'Medicamentos', 65.00);

-- Extras
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 3, 25, 'Restaurante carnaval', 220.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (2, 3, 13, 'Uber carnaval', 95.00);

-- -----------------------------------------------
-- Março/2025 (MesId = 3)
-- -----------------------------------------------
-- Fixas
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 1, 1, 'Aluguel março', 1500.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 1, 2, 'Condomínio março', 350.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 1, 10, 'Internet fibra', 120.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 1, 6, 'Plano celular', 55.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 1, 5, 'Spotify família', 34.90);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 1, 20, 'Plano de saúde', 280.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 1, 11, 'Prestação da moto', 650.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 1, 12, 'Seguro da moto', 89.90);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 1, 23, 'Curso de programação', 197.00);

-- Variáveis
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 2, 3, 'Conta de luz', 175.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 2, 4, 'Conta de água', 92.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 2, 7, 'Gás de cozinha', 110.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 2, 16, 'Supermercado semanal', 640.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 2, 14, 'Combustível', 280.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 2, 18, 'Hortifruti', 55.00);

-- Adicionais
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (3, 4, 24, 'Livro Clean Code', 75.00);

-- -----------------------------------------------
-- Abril/2025 (MesId = 4)
-- -----------------------------------------------
-- Fixas
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (4, 1, 1, 'Aluguel abril', 1500.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (4, 1, 2, 'Condomínio abril', 350.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (4, 1, 10, 'Internet fibra', 120.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (4, 1, 6, 'Plano celular', 55.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (4, 1, 5, 'Spotify família', 34.90);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (4, 1, 20, 'Plano de saúde', 280.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (4, 1, 11, 'Prestação da moto', 650.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (4, 1, 12, 'Seguro da moto', 89.90);

-- Variáveis
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (4, 2, 3, 'Conta de luz', 165.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (4, 2, 4, 'Conta de água', 85.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (4, 2, 16, 'Supermercado semanal', 590.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (4, 2, 14, 'Combustível', 260.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (4, 2, 19, 'Padaria', 70.00);

-- Extras
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (4, 3, 22, 'Consulta dentista', 350.00);

-- -----------------------------------------------
-- Maio/2025 (MesId = 5)
-- -----------------------------------------------
-- Fixas
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 1, 1, 'Aluguel maio', 1500.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 1, 2, 'Condomínio maio', 350.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 1, 10, 'Internet fibra', 120.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 1, 6, 'Plano celular', 55.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 1, 5, 'Spotify família', 34.90);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 1, 20, 'Plano de saúde', 280.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 1, 11, 'Prestação da moto', 650.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 1, 12, 'Seguro da moto', 89.90);

-- Variáveis
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 2, 3, 'Conta de luz', 155.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 2, 4, 'Conta de água', 82.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 2, 7, 'Gás de cozinha', 110.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 2, 16, 'Supermercado semanal', 610.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 2, 14, 'Combustível', 240.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 2, 17, 'Feira', 80.00);

-- Extras
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 3, 26, 'Cinema + pipoca', 85.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (5, 3, 25, 'Dia das mães restaurante', 195.00);

-- -----------------------------------------------
-- Junho/2025 (MesId = 6)
-- -----------------------------------------------
-- Fixas
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (6, 1, 1, 'Aluguel junho', 1500.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (6, 1, 2, 'Condomínio junho', 350.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (6, 1, 10, 'Internet fibra', 120.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (6, 1, 6, 'Plano celular', 55.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (6, 1, 5, 'Spotify família', 34.90);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (6, 1, 20, 'Plano de saúde', 280.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (6, 1, 11, 'Prestação da moto', 650.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (6, 1, 12, 'Seguro da moto', 89.90);

-- Variáveis
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (6, 2, 3, 'Conta de luz', 200.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (6, 2, 4, 'Conta de água', 90.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (6, 2, 16, 'Supermercado semanal', 650.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (6, 2, 14, 'Combustível', 270.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (6, 2, 9, 'Ração e petshop', 145.00);

-- Adicionais
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (6, 4, 28, 'Presente namorada', 250.00);

-- -----------------------------------------------
-- Janeiro/2026 (MesId = 7)
-- -----------------------------------------------
-- Fixas
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 1, 1, 'Aluguel janeiro', 1600.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 1, 2, 'Condomínio janeiro', 380.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 1, 10, 'Internet fibra', 130.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 1, 6, 'Plano celular', 55.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 1, 5, 'Spotify família', 34.90);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 1, 20, 'Plano de saúde', 310.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 1, 11, 'Prestação da moto', 650.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 1, 12, 'Seguro da moto', 95.00);

-- Variáveis
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 2, 3, 'Conta de luz', 220.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 2, 4, 'Conta de água', 98.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 2, 7, 'Gás de cozinha', 115.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 2, 16, 'Supermercado semanal', 680.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 2, 14, 'Combustível', 290.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 2, 19, 'Padaria', 90.00);

-- Extras
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (7, 3, 8, 'Mensalidade TV (novo plano)', 45.90);

-- -----------------------------------------------
-- Fevereiro/2026 (MesId = 8)
-- -----------------------------------------------
-- Fixas
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 1, 1, 'Aluguel fevereiro', 1600.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 1, 2, 'Condomínio fevereiro', 380.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 1, 10, 'Internet fibra', 130.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 1, 6, 'Plano celular', 55.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 1, 5, 'Spotify família', 34.90);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 1, 20, 'Plano de saúde', 310.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 1, 11, 'Prestação da moto', 650.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 1, 12, 'Seguro da moto', 95.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 1, 8, 'Mensalidade TV', 45.90);

-- Variáveis
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 2, 3, 'Conta de luz', 195.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 2, 4, 'Conta de água', 92.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 2, 16, 'Supermercado semanal', 660.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 2, 14, 'Combustível', 275.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 2, 17, 'Feira', 85.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 2, 21, 'Medicamentos gripe', 48.00);

-- Extras
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 3, 27, 'Viagem carnaval', 1200.00);
INSERT INTO Despesas (MesId, TipoDespesaId, SubCategoriaId, Descricao, Valor) VALUES (8, 3, 25, 'Restaurantes carnaval', 350.00);
