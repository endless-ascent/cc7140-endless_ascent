# Introdução

## Ideia principal do jogo

O jogo proposto é um rogue-like 2D em pixel art, fortemente inspirado na lenda da Torre de Babel, mas com uma interpretação única e original. A narrativa gira em torno de uma civilização que, ao construir uma torre colossal capaz de alcançar os céus, desperta a ira dos deuses. Como punição, os deuses fragmentam a linguagem da humanidade, dividindo a civilização em grupos incapazes de se comunicar entre si e assumindo o controle da torre.

Diante dessa tragédia, um guerreiro determinado surge com um propósito claro: escalar a torre, enfrentar os desafios impostos pelos deuses e recuperar os fragmentos da linguagem. Sua missão é restaurar a unidade e a comunicação entre os povos. No entanto, a jornada não é linear nem fácil. A torre é composta por diversos andares, cada um com obstáculos, inimigos e armadilhas, além de três chefões divinos posicionados estrategicamente nos andares 10, 20 e 30. Cada chefe guarda um fragmento do idioma original, necessário para reconstruir a integridade da civilização.

A progressão é baseada em tentativas e falhas. A cada nova escalada, o jogador controla um novo “sucessor”, herdeiro da vontade ancestral do guerreiro anterior. Essa vontade é representada por uma moeda coletada ao longo da jornada, usada na base da torre para aprimorar atributos, desbloquear novas habilidades e mudar de classe. Ao derrotar cada deus, o jogador também desbloqueia uma nova classe jogável, trazendo mais variedade estratégica e possibilidades de gameplay para futuras tentativas.

## Tipo de experiência desejada para o jogador

A experiência que se deseja proporcionar ao jogador é desafiadora, imersiva e recompensadora. Sendo um rogue-like, o jogo exige persistência, estratégia e adaptação constante. O jogador deve aceitar que falhar faz parte do processo e que o verdadeiro progresso está na evolução do personagem e na melhoria de suas habilidades a cada tentativa. O ciclo de morte e renascimento como um sucessor incentiva o jogador a aprender com os erros, adaptar suas escolhas de classe e aproveitar ao máximo os recursos disponíveis no acampamento-base.

Além da dificuldade progressiva, o jogo busca oferecer uma atmosfera rica em narrativa e ambientação. O sentimento de solidão, o mistério da torre e a grandiosidade do desafio diante dos deuses criam um ambiente denso e envolvente. O uso de pixel art e a trilha sonora imersiva reforçam essa ambientação, remetendo a clássicos do gênero ao mesmo tempo que imprime uma identidade própria.

Ao desbloquear novos fragmentos de idioma e novas classes, o jogador sente que está, de fato, reconstruindo algo maior do que si mesmo. Essa sensação de propósito, aliada à customização das builds e à diversidade de desafios — como puzzles, batalhas e plataformas — garante uma experiência rica, variada e emocionalmente significativa.

O jogo busca equilibrar dificuldade e narrativa, oferecendo ao jogador não apenas um desafio mecânico, mas também uma jornada simbólica de redenção, reconstrução e transcendência.

## Esboço do Game Loop

O jogo rodará em um ciclo contínuo, onde a lógica será constantemente verificada. Durante o loop, serão processadas as seguintes ações:
- Atualização da posição do jogador e dos inimigos.
- Detecção de colisões com plataformas, armadilhas e inimigos.
- Aplicação de dano e cálculo de morte para personagens.
- Verificação de eventos especiais, como salas de buffs ou chefes.
- Gerenciamento da sucessão de guerreiros em caso de morte.

#### Atores e Componentes
- Implementação de mecânicas de movimentação do guerreiro, incluindo pulo, ataque e esquiva.
- IA dos inimigos, determinando padrões de patrulha, perseguição e ataque.
- Comportamento dos deuses guardiões, cada um com habilidades únicas e desafios diferenciados.
- Elementos interativos, como plataformas móveis, alavancas e armadilhas.

#### Mecânica de Jogo
- Sistema de combate fluido, com variedade de armas e estilos de luta.
- Progressão de personagem, permitindo melhorias nos atributos e desbloqueio de novas habilidades.
- Seleção de buffs e armas aleatórias ao longo da jornada.
- Sistema de morte e sucessão, onde um novo guerreiro assume o lugar do anterior e pode herdar habilidades desbloqueadas.
- Gestão da ``Vontade Ancestral'' para compra de melhorias permanentes entre as tentativas.
