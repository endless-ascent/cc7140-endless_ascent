# Mecânica

## Elementos Formais do Jogo

### Padrão de Interação do Jogador
O jogador interage com o jogo por meio do teclado e mouse. As principais ações são:
- **Movimentação:**  
  - **W, A, S, D:** Controlam a locomoção do personagem pelos diferentes eixos.
- **Ações e Habilidades:**  
  - **Espaço:** Utilizado para o pulo.
  - **Clique Esquerdo do Mouse:** Realiza ataques básicos.
  - **L SHIFT:** Ativa o Dash, permitindo rápidas mudanças de direção e evasão de ataques.
- **Interação com Interface:**  
  - O cursor do mouse é utilizado para navegar em menus, selecionar opções durante as escolhas de cartas em andares especiais e interagir com elementos do acampamento.

### Objetivo do Jogo

#### Quando o Jogador Ganha?
- **Objetivo Final:**  
  O jogador vence quando consegue escalar a torre e derrotar o terceiro e último deus, localizado no andar 30, reunindo assim os fragmentos da linguagem e restaurando a comunicação entre os povos. A vitória é marcada por uma cutscene final que narra a redenção da civilização.

#### Quando o Jogador Perde?
- **Condição de Derrota:**  
  O jogador perde quando o personagem fica sem vida ou é derrotado em combate. A mecânica do jogo, típica de um rogue-like, permite que, mesmo após a derrota, o jogador recomece a jornada na base da torre como um novo sucessor, mantendo as melhorias adquiridas anteriormente através da “vontade ancestral”.

### Regras do Jogo

- **Movimentação e Controle:**
  - O personagem responde imediatamente aos comandos do teclado (W, A, S, D) para movimentação e ao espaço para pular.
  - O dash (L SHIFT) só pode ser utilizado após um breve tempo de recarga.
  - O clique esquerdo do mouse realiza ataques básicos com feedback visual imediato.

- **Interação e Combate:**
  - Colisões com plataformas e inimigos são detectadas a cada frame, determinando se o personagem toma dano ou consegue avançar.
  - O dano causado e recebido é calculado com base em atributos (força, defesa, vida e velocidade) que podem ser aprimorados ao longo da jornada.
  - Ao acertar inimigos, animações específicas (como chamas temporárias para aumento de força ou brilho para aumento de vida) confirmam o aprimoramento escolhido nos andares especiais.

- **Sistema de Decisão:**
  - Em andares especiais, o jogador deve escolher entre três cartas aleatórias, cada uma oferecendo aprimoramento de atributo ou habilidades.
  - A escolha é confirmada por animações e efeitos especiais, que evidenciam o impacto imediato e o efeito a longo prazo da decisão na árvore de habilidades.

- **Progressão e Dificuldade:**
  - Os níveis e desafios aumentam de dificuldade progressivamente, garantindo que o jogador precise constantemente aprimorar o personagem para avançar.
  - Chefões são encontrados nos andares 10, 20 e 30, exigindo estratégias específicas para cada encontro.

- **Recursos e Gestão:**
  - A “vontade ancestral” é a moeda do jogo, coletada ao derrotar inimigos e abrir baús secretos.
  - Esses recursos são utilizados no acampamento para comprar upgrades e trocar de classe, influenciando a estratégia e a personalização do personagem.

- **Regras de Interface:**
  - O jogador utiliza o mouse para interagir com menus, selecionar cartas e navegar pelos elementos do acampamento.
  - Elementos visuais, como minimapas e indicadores de inimigos, orientam o jogador e reforçam as decisões tomadas.

### Procedimentos do Jogo

- **Ciclo de Gameplay:**
  1. **Início na Base:** O jogador inicia no acampamento, onde pode usar a “vontade ancestral” para melhorar atributos e trocar de classe.
  2. **Ascensão pelos Andares:** A cada novo andar, o personagem enfrenta inimigos, puzzles e desafios de plataforma. Em andares especiais, surgem escolhas estratégicas que alteram a árvore de habilidades.
  3. **Encontros com Chefões:** Nos andares 10, 20 e 30, o jogador enfrenta chefões que testam todo o aprendizado acumulado e desbloqueiam novas classes.
  4. **Confronto Final:** Derrotar o último deus no andar 30 sela a vitória e exibe uma cutscene final que narra a restauração da comunicação na civilização.
  5. **Derrota e Recomeço:** Se o personagem for derrotado, o jogador retorna à base como um novo sucessor, podendo adquirir melhorias permanentes.

- **Procedimentos de Combate e Feedback:**
  - Toda ação (movimentação, ataque, dash) gera um feedback visual e sonoro que informa ao jogador que a ação foi executada.
  - O sistema de colisões é constantemente atualizado, permitindo respostas imediatas aos ataques e impactos durante os combates.

### Recursos do Jogo

- **Recursos Interativos:**
  - Personagens controláveis e inimigos com IA, que se adaptam aos desafios dos diferentes andares.
  - Itens colecionáveis e baús secretos espalhados pela torre.
- **Recursos Visuais e Sonoros:**
  - Pixel art detalhada, com animações fluidas para cada ação.
  - Trilha sonora adaptativa e efeitos sonoros sincronizados com as ações do jogo.
- **Sistema de Upgrades:**
  - Árvore de habilidades e atributos modificáveis através de escolhas em andares especiais.
  - A “vontade ancestral” como recurso para evoluir o personagem e personalizar o estilo de jogo.

### Limites do Jogo

- **Limites Técnicos:**
  - O jogo opera em um ambiente 2D, com restrições impostas pela mecânica de plataformas e colisões.
  - As interações e movimentações são restritas ao espaço definido pela torre e pelos obstáculos presentes em cada andar.
- **Limites de Progresso:**
  - O avanço é limitado pela dificuldade progressiva, forçando o jogador a utilizar seus recursos e melhorias de forma estratégica para superar os desafios.
  - As escolhas em andares especiais estabelecem um caminho único na árvore de habilidades, limitando algumas possibilidades futuras com base na decisão tomada.

### Resultados do Jogo

#### Resultado Após a Vitória
- **Conquista da Torre:**
  - Ao derrotar o terceiro deus no andar 30, o jogo exibe uma cutscene final onde os fragmentos da linguagem são reunidos.
  - A narrativa final revela a restauração da comunicação entre os povos, simbolizando a redenção da civilização.
  - A vitória é celebrada com efeitos visuais e uma trilha sonora épica, reforçando o sentimento de conquista e superação.

#### Resultado Após a Derrota
- **Recomeço com Aprendizado:**
  - Quando o personagem é derrotado, o jogador não reinicia do zero, mas sim retorna à base da torre como um novo sucessor.
  - O sistema preserva as melhorias adquiridas (por meio da “vontade ancestral”), permitindo que cada nova tentativa seja ligeiramente mais robusta.
  - A derrota é apresentada como parte do ciclo de progresso do jogo, incentivando o jogador a ajustar estratégias e aprimorar suas habilidades para enfrentar os desafios futuros.

Esta estrutura mecânica combina uma interação fluida, regras bem definidas e procedimentos claros, garantindo que o jogador entenda o que deve fazer e como suas ações impactam o jogo tanto a curto quanto a longo prazo. Cada elemento, desde os controles até os feedbacks e a progressão, trabalha em conjunto para oferecer uma experiência desafiadora e recompensadora.
