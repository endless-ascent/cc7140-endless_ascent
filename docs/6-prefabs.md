# Prefabs

## Prefab 1 
### Nome
**Herói das Eras**

### Descrição
Representa o personagem controlado pelo jogador. Pode assumir diferentes classes com habilidades únicas: Guerreiro, Arqueiro e Mago.

### Quando são utilizados
Sempre que o jogador inicia uma nova run ou decide trocar de classe.

### Quais seus componentes

#### Sprites
- [Miniature Army 2D V.1 Medieval Style](https://assetstore.unity.com/packages/2d/characters/miniature-army-2d-v-1-medieval-style-72935#content)
- ![image](https://github.com/user-attachments/assets/b9913aaf-d632-4c97-b782-674922ae5a0e)


#### Colisores
- Box Collider 2D
- Circle Collider 2D

#### Scripts
- **PlayerController.cs**: movimenta o personagem com base nos inputs do jogador (WASD, espaço, mouse). Responsável por transições de animações, velocidade, pulo, dash, etc.
- **AttackSystem.cs**: executa ataques distintos para cada classe, lidando com tempo de recarga, animações de ataque e efeitos visuais.
- **HealthManager.cs**: gerencia os pontos de vida do jogador, detecta dano, ativa efeitos visuais/sonoros ao sofrer dano e controla o comportamento de morte.
- **ClassSwitcher.cs**: troca o sprite e os atributos do jogador com base na classe selecionada (vida, dano, velocidade, etc).

---

## Prefab 2 
### Nome
**Protetores da Torre**

### Descrição
Entidades hostis variadas que atacam o jogador. Possuem diferentes aparências e padrões de comportamento.

### Quando são utilizados
Durante o combate nos andares comuns.

### Quais seus componentes

#### Sprites
- ![image](https://github.com/user-attachments/assets/0b6691bd-92ed-436d-9905-906df54687c5)


#### Colisores
- Box Collider 2D ou Polygon Collider 2D (varia por inimigo)

#### Scripts
- **EnemyAI.cs**: controla a movimentação automática, patrulhamento ou perseguição do jogador com base na distância.
- **AttackBehavior.cs**: determina o tipo de ataque (curto, longo alcance, explosivo, etc) e seu alcance.
- **HealthManager.cs**: semelhante ao do jogador; define vida, reação ao dano e morte.
- **DropLoot.cs**: define o comportamento de drop aleatório de itens ou moedas ao morrer.

---

## Prefab 3 
### Nome
**Deuses da Torre**

### Descrição
Chefes hostis que possuem mecânicas e padrões distintos, representando os deuses que guardam os fragmentos de idioma.

### Quando utilizados
Nos andares de chefes (10, 20 e 30).

### Quais seus componentes

#### Sprites
- ![image](https://github.com/user-attachments/assets/4a4eacbc-8c9c-477a-96ef-e837a049fdd9)


#### Colisores
- Trigger Collider 2D
- Box Collider 2D
- Polygon Collider 2D

#### Scripts
- **BossAI.cs**: define comportamentos complexos de batalha, fases diferentes no combate, leitura da posição do jogador, e execução de padrões de ataque.
- **AttackBehavior.cs**: semelhante ao dos inimigos, porém com variações mais complexas e efeitos especiais.
- **HealthManager.cs**: controle da vida e fases do boss (ex: segunda fase ao chegar em 50% de vida).
- **DropLoot.cs**: define o drop de itens importantes como fragmentos de idioma e recompensas raras.

---

## Prefab 4 
### Nome
**Cenário** (background, foreground)

### Descrição
Conjunto de elementos visuais e físicos para construção dos andares, ambientes, armadilhas e objetos interativos do jogo.

### Quando utilizados
Durante todas as cenas jogáveis.

### Quais seus componentes

#### Sprites
- Multi Platformer Tileset by Shackhal
- ![image](https://github.com/user-attachments/assets/41deb0f7-8d30-45d3-b03e-f9ab7c7e58ca)
- ![image](https://github.com/user-attachments/assets/52f200d6-4429-4efe-8b3e-cc597b276956)

- Lava Caves - Fantasy Pixel Art Tileset by aamatniekss
- ![image](https://github.com/user-attachments/assets/cfcce7f7-5f01-4266-b88a-03200380d3dc)
- ![image](https://github.com/user-attachments/assets/b49985ee-1e0d-43c4-b251-706e731154ac)

- Pixel Art Platformer - Village Props
- ![image](https://github.com/user-attachments/assets/7162a0d1-d922-41c2-aa21-4447a6d6b101)

- Rogue Fantasy Castle
- [FREE] 2D Hand Painted
- 2D Death Traps - Free
- Medieval Pixel Art Asset FREE
- WINGS FOR YOUR GAME

#### Colisores
- Box Collider 2D

#### RigidBody
- RigidBody 2D

#### Scripts
- **Walls.cs**: define colisão com paredes e estruturas físicas.
- **Foreground_decor.cs**: carrega elementos visuais do primeiro plano para dar profundidade e ambientação.
- **Platform.cs**: cria plataformas sólidas ou móveis com comportamento de suporte ao jogador.
- **Trap.cs**: ativa armadilhas físicas ao detectar o jogador (ex: espinhos, serras, blocos que caem).
- **Trap_projectile.cs**: dispara projéteis como flechas ou bolas de fogo em padrões definidos.

---

## Prefab 5 
### Nome
**Itens**

### Descrição
Itens que podem ser coletados, comprados ou utilizados durante a jogabilidade. Incluem comida para cura, armas e itens de aprimoramento.

### Quando utilizados
Ao derrotar certos inimigos, abrir baús ou comprar na base/acampamento.

### Quais seus componentes

#### Sprites
- Free Pixel Food
- Free Game Items
- ![image](https://github.com/user-attachments/assets/63653d58-3dc1-4d22-9d5b-23e18e9cc230)


#### Colisores / Física
- RigidBody 2D

#### Scripts
- **Items_food.cs**: define o comportamento de itens de cura. Ao colidir com o jogador, aumenta a vida e exibe um efeito de cura.
- **Items_weapons.cs**: define o funcionamento de armas temporárias ou adicionais que o jogador pode pegar.
- **Items_collectables.cs**: controla itens colecionáveis como moedas, “vontade ancestral”, pergaminhos, etc.
