const eyeIcon = document.querySelector('.mempool-container svg');
async function loadMempool() {
    try {
        const transactionsDiv = document.getElementById('transactions');
        const response = await fetch('http://localhost:5000/api/v1.0/transaction/mempool', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const transactions = await response.json();
        
        transactionsDiv.innerHTML = ''; 

        transactions.forEach(transaction => {
            const transactionElement = document.createElement('div');
            transactionElement.className = 'transaction-item mb-3 p-3 border border-indigo-200 rounded-md';
            transactionElement.innerHTML = `
                <div class="flex flex-col gap-1 text-sm">
                    <div class="font-medium text-gray-700">From: ${transaction.senderWalletName}</div>
                    <div class="font-medium text-gray-700">To: ${transaction.recipientWalletName}</div>
                    <div class="font-semibold text-indigo-600">Amount: ${transaction.amount}</div>
                </div>
            `;
            transactionsDiv.appendChild(transactionElement);
        });

    } catch (error) {
        console.error('Error fetching mempool:', error);
        transactionsDiv.innerHTML = `
            <div class="text-red-500 p-3">
                Failed to load transactions. Please try again.
            </div>
        `;
    }
}

async function renderMempool() {
    try {  
        await loadMempool();
        eyeIcon.classList.add('animate-pulse');
        setTimeout(() => eyeIcon.classList.remove('animate-pulse'), 1000);
    } catch (error) {
        console.error('Error fetching mempool:', error);
    }
}


eyeIcon.addEventListener('click', async () => {
    await renderMempool();
});


eyeIcon.style.cursor = 'pointer';
eyeIcon.addEventListener('mouseover', () => {
    eyeIcon.style.fill = '#6366f1';
});
eyeIcon.addEventListener('mouseout', () => {
    eyeIcon.style.fill = '#000000'; 
});

export { renderMempool };