import {renderMempool} from './mempool.js';

async function mineBlock() {
    try {
        const response = await fetch('http://localhost:5000/api/v1.0/blockchain/mine', {
            method: 'POST',
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        alert('Block mined successfully!');
        await loadBlockchain();
    } catch (error) {
        console.error('Error mining block:', error);
        alert('Failed to mine a block.');
    }
}

async function loadBlockchain() {
    try {
        const response = await fetch('http://localhost:5000/api/v1.0/blockchain/fullchain');
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const fullchain = await response.json();
        renderBlockchain(fullchain);
        renderMempool();
    } catch (error) {
        console.error('Error fetching blockchain data:', error);
    }
}

function renderBlockchain(blockchain) {
    const container = document.querySelector('.blockchain-container');
    container.innerHTML = ''; 

    if (!blockchain.length) {
        container.innerHTML = '<p>No blocks available in the chain.</p>';
        return;
    }

    blockchain.forEach(block => {
        const blockElement = document.createElement('div');
        blockElement.classList.add('block');
        blockElement.innerHTML = `
            <h3>Block #${block.index}</h3>
            <p><strong>Timestamp:</strong> ${new Date(block.timeStamp).toLocaleString()}</p>
            <p><strong>Proof:</strong> ${block.proof}</p>
            <p><strong>Merkle Root:</strong> ${block.merkleRoot}</p>
            <p><strong>Previous Hash:</strong> ${block.previousHash || 'None'}</p>
        `;
        container.appendChild(blockElement);
    });
}

document.addEventListener('DOMContentLoaded', () => {
    loadBlockchain()
    const minerImage = document.querySelector('img[alt="Miner"]');
    if (minerImage) {
        minerImage.addEventListener('click', mineBlock);
    }
});
